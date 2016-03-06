using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using GreatSQL.Models;

namespace GreatSQL.Results
{
    public class SimpleIdentity: IIdentity
    {
        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public User User { get; set; }
    }
}