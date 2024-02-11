using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public class AuthTokenRepository : AuthService
    {
        public AuthTokenRepository(string encryptionKey = null)
        {
            _encryptionKey = encryptionKey ?? "SecretKeyTaskApplication@1345";
        }

        public override string TokenEncryptionKey => _encryptionKey 
            ?? throw new NullReferenceException("Token encryption key can't be null");

        public override string GenerateAccessToken(Dictionary<string, object> claims,int timeInMinutes)
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

        public override string GenerateRefreshToken(int tokenSize)
        {
            var randomNumber = new byte[tokenSize];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshtoken = Convert.ToBase64String(randomNumber);
            return refreshtoken;
        }

        public override ClaimsPrincipal VerifyToken(string token)
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

        private readonly string _encryptionKey;
    }
}
