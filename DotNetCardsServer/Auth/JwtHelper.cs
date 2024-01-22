using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetCardsServer.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCardsServer.Auth
{
    public class JwtHelper
    {
        public readonly static string secretKey = "7B9B325A-B363-4D15-B4E5-B4DE0F2B1929";

        public static string GenerateAuthToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("isAdmin", user.IsAdmin.ToString()),
                new Claim("isAdmin", user.IsAdmin.ToString()),
                new Claim("first", user.UserName.FirstName.ToString()),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "CardsServer",
                audience: "CardReactFront",
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
