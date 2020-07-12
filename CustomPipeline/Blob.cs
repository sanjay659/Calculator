using CustomPipeline.Enum;
using CustomPipeline.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPipeline
{
    public class Blob : IBlob
    {
        public void UploadRequest(LoggingEnum loggingEnum, HttpContext httpContext, string value = "")
        {
            throw new NotImplementedException();
        }

        public void UploadResponse(LoggingEnum loggingEnum, string response)
        {
            throw new NotImplementedException();
        }
    }
}
