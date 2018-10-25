using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        DataContext _context;

        public AuthenticationController(DataContext context)
        {
            _context = context;
        }


        // POST api/<controller>
        [HttpPost]
        public IActionResult Post()
        {
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValues = header.ToString().Substring("Basic ".Length).Trim();
                var userNameAndPasswordEncoded = Encoding.UTF8.GetString(Convert.FromBase64String(credValues));
                var userNameAndPassword = userNameAndPasswordEncoded.Split(":");

                //check db for mail and password
                UserModel user = null;
                foreach(UserModel u in _context.Users)
                {
                    if(u.EmailAddress == userNameAndPassword[0] && u.Password == userNameAndPassword[1])
                    {
                        user = u;
                        break;
                    }
                }
                
                if ( user!=null)
                {
                    var claimsData = new[] { new Claim(ClaimTypes.Name, "username") };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersupersupersecretKey"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                        issuer: "mysite.com",
                        audience: "mysite.com",
                        expires: DateTime.Now.AddMinutes(30),
                        claims: claimsData,
                        signingCredentials: signInCred
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
                return BadRequest("Wrong username or password");
            }

            return BadRequest("Wrong request");
            
        }

    }
}
