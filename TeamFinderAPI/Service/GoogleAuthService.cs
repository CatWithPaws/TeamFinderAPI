using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace TeamFinderAPI.Service
{
    public class GoogleAuthService
    {

        private const string ClientSecret = "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";
        private const string ClientId = "384010096834-b2nqf1gfe13v90nfiglkqcpgd0a73deh.apps.googleusercontent.com";
        private const string OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string TokenServerEndpoint = "https://oauth2.googleapis.com/token";

        public static string GenerateOAuthRequestUrl(string scope, string redirectUrl, string codeChellange)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "redirect_uri", redirectUrl },
                { "response_type", "code" },
                { "scope", scope },
                { "code_challenge", codeChellange },
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };

            var url = QueryHelpers.AddQueryString(OAuthServerEndpoint, queryParams);
            return url;
        }

        
    }
}