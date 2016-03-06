using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GreatSQL.Enums;
using GreatSQL.Models;
using GreatSQL.Results;

namespace GreatSQL.Filters
{
    public class RuleAuthorizationAttribute: Attribute, IAuthorizationFilter
    {
        public Rule Rule { get; }

        public RuleAuthorizationAttribute(Rule rule)
        {
            Rule = rule;
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            // 尝试获取认证后获得的授权信息
            var simplePrincipal = actionContext.RequestContext.Principal as SimplePrincipal;

            if (simplePrincipal == null)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) {ReasonPhrase = "No Authentication"};

            var id = simplePrincipal.Identity as SimpleIdentity;

            var db = new GreatSQLContext();

            var ruleGroup = await db.Groups.FindAsync(cancellationToken, id.User.RuleGroupID);

            var userRule = (Rule)ruleGroup.Rule;

            return (userRule & Rule) == userRule
                ? await continuation() 
                : new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Group" };
        }

        public bool AllowMultiple => false;
    }
}