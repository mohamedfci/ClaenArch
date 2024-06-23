﻿using ClaenArch.Services;
using Domains.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ClaenArch.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        //public string GenerateJwtToken(TblUsers user)
        //{
        //    // var tokenHandler = new JwtSecurityTokenHandler();
        //    //  var key = Encoding.ASCII.GetBytes(_jwtSettings.SymmetricSecurityKey);

        //    //    var tokenDescriptor = new SecurityTokenDescriptor
        //    //    {
        //    //        Subject = new ClaimsIdentity(new Claim[]
        //    //        {
        //    //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    //        new Claim(ClaimTypes.Name, user.UserName),
        //    //        new Claim(ClaimTypes.Role, user.Role)
        //    //        }),
        //    //        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes), // Token expiry
        //    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    //    };

        //    //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //    return tokenHandler.WriteToken(token);
        //    //}


        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.SymmetricSecurityKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //  new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
        //        Audience = _jwtSettings.ValidAudience, // Set the audience
        //        Issuer = _jwtSettings.ValidIssuer,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}
        public string GenerateJwtToken(TblUsers user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SymmetricSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                  new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = _jwtSettings.ValidIssuer,
                Audience = _jwtSettings.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}