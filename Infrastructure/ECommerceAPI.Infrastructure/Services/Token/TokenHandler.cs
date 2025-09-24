using ECommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int minute)
        {
            Application.DTOs.Token token = new Application.DTOs.Token();

            //SecurityKey'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Expiration = DateTime.Now.AddMinutes(minute);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıf örneği
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
