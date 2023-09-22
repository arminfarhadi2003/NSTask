using Microsoft.IdentityModel.Tokens;
using NSTask.Models.Dtos;
using NSTask.Services.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSTask.Services
{
    public class TokenService:ITokenService
    {
        public Task<TokenResultDto> CreateToken(Guid id, string Email)
        {
            SecurityHelper securityHelper = new SecurityHelper();


            var claims = new List<Claim>
                {
                    new Claim ("UserId", id.ToString()),
                    new Claim ("Name", Email),
                };

            string key = "{16D9BBF8-FA00-4D89-9BB5-99610E95BA70}";
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(
                issuer: "test",
                audience: "test",
                expires: tokenexp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid();

            var RefreshTokenExp = DateTime.Now.AddDays(30);

            var tokenResult = new TokenResultDto
            {
                Token = jwtToken,
                TokenExp = tokenexp,
            };


            return Task.FromResult(tokenResult);
        }
    }
}
