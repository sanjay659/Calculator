using CustomPipeline.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPipeline.Interface
{
    public interface IBlob
    {
          
        void UploadRequest(LoggingEnum loggingEnum, HttpContext httpContext, string value = "");

        void UploadResponse(LoggingEnum loggingEnum, string response);
    }

}
