using Mice.IdentityModels.DtoModels;
using Mice.IdentityModels.EntityModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityServices.Helpers
{
    public class TokenHelper
    {
        public string HashPassword(string ClearText)
        {
            if (ClearText == null) ClearText = "";
            byte[] ByteData = Encoding.ASCII.GetBytes(ClearText);
            //MD5 nesnesi oluŞturalİm.
            MD5 oMd5 = MD5.Create();
            //Hash deðerini hesaplayalİm.
            byte[] HashData = oMd5.ComputeHash(ByteData);

            //byte dizisini hex formatİna çevirelim
            StringBuilder oSb = new StringBuilder();
            for (int x = 0; x < HashData.Length; x++)
            {
                //hexadecimal string deðeri
                oSb.Append(HashData[x].ToString("x2"));
            }
            return oSb.ToString();
        }

        public Token GenerateToken(CompanyUser user,string jwtKey="",string issuer="")
        {
            Token token = new Token();
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("Email", user.Email),
                        new Claim("LastName",user.LastName),
                        new Claim("CompanyId", user.CompanyId.ToString()),
                    };
            token.Expiration = DateTime.UtcNow.AddHours(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokn = new JwtSecurityToken(
                issuer,
                null,
                claims,
                expires: token.Expiration,
                signingCredentials: signIn);
            token.Access_Token= new JwtSecurityTokenHandler().WriteToken(tokn);
            return token;
        }
        public Token GenerateCustomerToken(Customer user, string jwtKey = "", string issuer = "")
        {
            Token token = new Token();
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("Email", user.Email),
                        new Claim("LastName",user.LastName),
                    };
            token.Expiration = DateTime.UtcNow.AddHours(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokn = new JwtSecurityToken(
                issuer,
                null,
                claims,
                expires: token.Expiration,
                signingCredentials: signIn);
            token.Access_Token = new JwtSecurityTokenHandler().WriteToken(tokn);
            return token;
        }
    }
}
