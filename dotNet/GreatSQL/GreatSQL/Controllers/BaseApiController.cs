using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GreatSQL.Enums;
using GreatSQL.Filters;
using GreatSQL.Models;
using GreatSQL.Results;

namespace GreatSQL.Controllers
{
    [BasicAuthentication]
    public abstract class BaseApiController : ApiController
    {
        protected GreatSQLContext db = new GreatSQLContext();

        protected SimpleIdentity Identity
        {
            get
            {
                var simplePrincipal = base.User as SimplePrincipal;

                return simplePrincipal?.Identity as SimpleIdentity;
            }
        }

        protected new User User => Identity?.User;

        protected Rule UserRule => (Rule) User.RuleGroup.Rule;
    }
}