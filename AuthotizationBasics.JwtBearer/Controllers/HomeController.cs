
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthotizationBasics.JwtBearer.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,"test"),
                new Claim(JwtRegisteredClaimNames.Email,"test@test.ru")
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
            var key = new SymmetricSecurityKey(secretBytes);
            var  signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Constants.Issuer, Constants.Audience, claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(55),signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            ViewBag.token = value;
            return View();
        }
    }
}
