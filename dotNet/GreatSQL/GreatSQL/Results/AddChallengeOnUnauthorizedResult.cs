using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GreatSQL.Results
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        /// <summary>
        /// 构造一个未授权的结果
        /// </summary>
        /// <param name="challenge">质询？</param>
        /// <param name="innerResult">内部查询？</param>
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            Challenge = challenge;
            InnerResult = innerResult;
        }

        public AuthenticationHeaderValue Challenge { get; }

        public IHttpActionResult InnerResult { get; }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // 先执行内部查询
            var response = await InnerResult.ExecuteAsync(cancellationToken);

            // 如果不是未授权的，则通过
            if (response.StatusCode != HttpStatusCode.Unauthorized) return response;
            
            // Only add one challenge per authentication scheme.
            if (response.Headers.WwwAuthenticate.All(h => h.Scheme != Challenge.Scheme))
                response.Headers.WwwAuthenticate.Add(Challenge);
            
            return response;
        }
    }
}