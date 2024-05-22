using JDA.Core.Utilities;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Tokens
{
    public class TokenHelper
    {
        public static TokenResult GetToken(List<Claim> claims)
        {
            string issuer = AppSettingsHelper.GetConfig("Jwt:Issuer");
            string audience = AppSettingsHelper.GetConfig("Jwt:Audience");
            string issuerSigningKey = AppSettingsHelper.GetConfig("Jwt:IssuerSigningKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new TokenResult() { Token = token, ExpireTime = 60 };
        }
    }
}
