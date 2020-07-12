using CustomPipeline.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPipeline
{
    public class InterfaceLog : IInterfaceLog
    {
        public InterfaceLog()
        { 
        }
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
    }
}
