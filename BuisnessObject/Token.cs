using BuisnessEntities;
using BuisnessObject.Interface;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessObject
{
    public class Token : IToken
    {
        private readonly ITokenDao _tokenDao;

        public Token(ITokenDao tokenDao)
        {
            _tokenDao = tokenDao;
        }
        public UserContext GetUsercontext(string operationname, string token)
        {
            return _tokenDao.GetUsercontext(operationname);
        }
    }
}
