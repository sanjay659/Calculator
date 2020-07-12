using BuisnessObject.Interface;
using CustomPipeline;
using CustomPipeline.Enum;
using CustomPipeline.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Calculator.MiddleLayer
{
    public class ResponseHandler
    {
        private readonly RequestDelegate _next;

        public ResponseHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IInterfaceLog interfaceLog, IBlob blob)
        {
            try
            {
                //First, get the incoming request
                interfaceLog.RequestJson = await FormatRequest(context.Request);

                //Copy a pointer to the original response body stream
                var originalBodyStream = context.Response.Body;

                //Create a new memory stream...
                using (var responseBody = new MemoryStream())
                {
                    //...and use that for the temporary response body
                    context.Response.Body = responseBody;

                    await _next(context);

                    //Format the response from the server
                    interfaceLog.ResponseJson = await FormatResponse(context.Response);

                    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                    await responseBody.CopyToAsync(originalBodyStream);
                }

                blob.UploadResponse(LoggingEnum.Response, interfaceLog.ResponseJson);

               
            }
            catch (Exception exception)
            {
               // serviceError.LogError(exception, GetType().Namespace + " - Error Occured in " + MethodBase.GetCurrentMethod());
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            //var body = request.Body;

            if (request.Method == "POST" || request.Method == "PUT" || request.Method == "DELETE")
            {
                return await ReturnBody(request);
            }

            if (request.Method == "GET")
            {
                if (request.ContentLength > 0)
                {
                    return await ReturnBody(request);
                }

                return request.QueryString.Value;
            }

            return string.Empty;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{responseBody}";
        }


        public async Task<string> ReturnBody(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            //request.EnableRewind();

            var bodyReader = new StreamReader(request.Body);

            var requestBody = await bodyReader.ReadToEndAsync();

            var requestHeaders = request.Headers;

            var headers = requestHeaders.Keys.Aggregate(string.Empty, (current, key) =>
                current + key + "=" + requestHeaders[key] + Environment.NewLine);

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            //var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //////...Then we copy the entire request stream into the new buffer.
            //await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //////We convert the byte[] into a string using UTF8 encoding...
            //var bodyAsText = Encoding.UTF8.GetString(buffer);

            //////..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            ////request.Body = body;
            request.Body.Seek(0, SeekOrigin.Begin);

            return $"{headers}\n{requestBody}";
        }

    }

    public static class CustomMiddleLayer
    {
        public static IApplicationBuilder MyMiddleLayer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseHandler>();
        }
    }

}
