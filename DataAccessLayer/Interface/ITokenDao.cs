using BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interface
{
    public interface ITokenDao
    {
        UserContext GetUsercontext(string opertionname);
    }
}
