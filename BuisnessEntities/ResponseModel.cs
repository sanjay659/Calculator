using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BuisnessEntities
{
    public class ResponseModel
    {
        public ResponseModel()
        { 
        }

        [DataMember]
        public object Data { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public List<ErrorModel> ErrorDetails { get; set; }
        [DataMember]
        public List<SuccessModel> SuccessDetails { get; set; }
    }
    public class ErrorModel
    {

        public string Key { get; set; }
        public List<Error> Errors { get; set; }
    }
    public class SuccessModel
    {
        public string Key { get; set; }
    }
    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
