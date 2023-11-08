using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public class AuthTokenRepository :IAuthToken
    {
        public AuthTokenRepository(string securitykey)
        {
            _securityToken = securitykey;
        }
        public string GenerateAccessToken(Guid userId, string email)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>()
                {
                    { "id", userId.ToString("N") },
                    { ClaimTypes.Email,email }
                },
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshtoken = Convert.ToBase64String(randomNumber);
            return refreshtoken;
        }

        public UserDTO VerifyToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            try
            {
                var claims = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var userId = claims.FindFirst("id").Value;
                var email = claims.FindFirst(ClaimTypes.Email).Value;

                var userDto = new UserDTO(userId, email);
                return userDto;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Encoding.UTF8.GetBytes(_securityToken);
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

        private readonly string _securityToken;
    }
}
