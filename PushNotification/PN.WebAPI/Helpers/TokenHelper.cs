using Microsoft.IdentityModel.Tokens;
using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PN.WebAPI.Helpers
{
   public class TokenHelper
   {
      public static string GenerateToken(User user, string secretKey)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new System.Security.Claims.ClaimsIdentity(
               new Claim[]
               {
                  new Claim(ClaimTypes.Name,user.id.ToString()),
               }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);

         return tokenHandler.WriteToken(token);
      }
   }
}
