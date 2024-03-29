﻿using Microsoft.IdentityModel.Tokens;
using RestWithASPNETUdemy.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private TokenConfiguration _configuration;

        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // Represents the cryptographic key and security algorithms that are used to generate a digital signature.

            var options = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32]; 
            using (var rng = RandomNumberGenerator.Create()) // Esse metodo create() criar uma nova estancia da classe RandomNumberGenerator
            {
                rng.GetBytes(randomNumber); // Que preencher o array de byte[] randomNumber com numeros incriptografados 
                return Convert.ToBase64String(randomNumber); // Que depois são convertidos para uma string com o metodo ToBase64String() que mexer com o decode Base64
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY_SUPER_SECRET_KEY")),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}
