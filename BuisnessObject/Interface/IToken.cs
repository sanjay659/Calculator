using BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessObject.Interface
{
    public interface IToken
    {
        UserContext GetUsercontext(string operationname, string token);
    }
}
