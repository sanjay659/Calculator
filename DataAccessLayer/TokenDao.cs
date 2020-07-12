using BuisnessEntities;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class TokenDao : ITokenDao
    {
        public UserContext GetUsercontext(string opertionname)
        {
            UserContext userContext = new UserContext();
            userContext.Token = "ValidToken";
            userContext.UserName = "ValidUser";
            userContext.BuyerPartnerCode = 100;

            return userContext;
        }
    }
}
