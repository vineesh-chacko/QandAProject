using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QandAServiceLayer;
using QandAViewModels;



namespace QanAProject.ApiControllers
{
    public class AccountController : ApiController
    {
        IUsersService us;

        public AccountController(IUsersService us)
        {
            this.us = us;
        }
        public string Get(string Email)
        {
            if (this.us.GetUsersByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }
        }
    }
}
