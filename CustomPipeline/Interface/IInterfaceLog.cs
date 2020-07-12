using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPipeline.Interface
{
   public interface IInterfaceLog
    {
        string RequestJson { get; set; }
        string ResponseJson { get; set; }
    }
}
