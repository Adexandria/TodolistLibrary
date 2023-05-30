﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class AuthTokenRepository :IAuthToken
    {
        public string GenerateAccessToken(User user)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>()
                {
                    { "id", user.Id.ToString("N") }
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

        public string VerifyToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            try
            {
                var claims = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var userId = claims.FindFirst("id").Value;
                return userId;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Encoding.UTF8.GetBytes("SecretKeyTaskApplication@1345");
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
