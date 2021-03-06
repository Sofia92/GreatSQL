﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using GreatSQL.Models;
using GreatSQL.Results;

namespace GreatSQL.Filters
{
    public class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            // 如果没认证信息，或类型不同，放弃
            if (authorization == null || authorization.Scheme != "Basic")
                return;

            // 没有认证参数~ 放弃
            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            // 解析用户名密码失败~ 放弃
            var userNameAndPasword = ExtractUserNameAndPassword(authorization.Parameter);
            if (userNameAndPasword == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
                return;
            }

            var email = userNameAndPasword.Item1;
            var password = userNameAndPasword.Item2;

            // 查询登录的用户是谁
            var db = new GreatSQLContext();
            var userQuery = from u in db.Users
                where u.Email == email && u.Password == password
                select u;

            var user = await userQuery.FirstOrDefaultAsync(cancellationToken);
            
            // 找不到？放弃
            if (user == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
                return;
            }

            // var a = new ClaimsPrincipal(new[] { new ClaimsIdentity(new [] { new Claim(ClaimTypes.Name, user.Name) }, "Basic") });

            // 最终认证成功，记录授权和认证信息
            context.Principal = new SimplePrincipal
            {
                Identity = new SimpleIdentity
                {
                    Name = user.Email,
                    AuthenticationType = "Basic",
                    IsAuthenticated = true,
                    User = user
                }
            };
        }

        private static Tuple<string, string> ExtractUserNameAndPassword(string authorizationParameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (string.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            var colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            var userName = decodedCredentials.Substring(0, colonIndex);
            var password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new AddChallengeOnUnauthorizedResult(new AuthenticationHeaderValue("Basic", "realm=\"GreatSQL\""), context.Result);
            return Task.FromResult(0);
        }
    }
}