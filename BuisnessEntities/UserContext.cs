using System;

namespace BuisnessEntities
{
    public class UserContext
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public int Email { get; set; }

        public long BuyerPartnerCode { get; set; }

    }
}
