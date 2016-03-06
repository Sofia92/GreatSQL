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

            if (id == null)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) {ReasonPhrase = "No Authentication"};

            var db = new GreatSQLContext();

            var ruleGroup = await db.Groups.FindAsync(cancellationToken, id.User.RuleGroupID);

            // 更新认证数据
            id.User.RuleGroup = ruleGroup;

            var userRule = (Rule)ruleGroup.Rule;

            if ((userRule & Rule) == 0)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Unauthorized Group" };

            var responseMessage = await continuation();

            return responseMessage;
        }

        public bool AllowMultiple => false;
    }
}