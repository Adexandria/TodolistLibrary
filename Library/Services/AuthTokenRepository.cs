using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public class AuthTokenRepository : IAuthToken
    {
        public string TokenEncryptionKey => "SecretKeyTaskApplication@1345"
            ?? throw new NullReferenceException("Token encryption key can't be null");

        public string GenerateAccessToken(Dictionary<string, object> claims,int timeInMinutes)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(timeInMinutes),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public string GenerateRefreshToken(int tokenSize)
        {
            var randomNumber = new byte[tokenSize];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshtoken = Convert.ToBase64String(randomNumber);
            return refreshtoken;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            try
            {
                var claims = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return claims;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Encoding.UTF8.GetBytes(TokenEncryptionKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
