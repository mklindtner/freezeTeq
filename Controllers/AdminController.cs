using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using freezeapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace freezeapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        public AdminController() { }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] User User)
        {
            // TODO: Authenticate Admin with Database
            // If not authenticate return 401 Unauthorized
            // Else continue with below flow

            //instead the user should be checked with database
            if (User.Username != "mkl" || User.Password != "123")
            {
                return new UnauthorizedResult();
            }

            var Claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var Token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                Claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult GenerateBadge([FromBody] Agent Agent)
        {
            var Claims = new List<Claim>
            {
                new Claim("type", "Agent"),
                new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var Token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                Claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);
            return new OkObjectResult(jwt);
        }
    }
}