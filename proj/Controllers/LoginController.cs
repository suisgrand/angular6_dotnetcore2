using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using proj.IRepositories;
using proj.JWT;
using proj.Models;
using static proj.Controllers.SampleDataController;

namespace proj.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginRepository repository;
        private readonly AppSettings _appSettings;

        public LoginController(ILoginRepository repository, IOptions<AppSettings> appSettings)
        {
            this.repository = repository;
            _appSettings = appSettings.Value;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Login>> GetLogins()
        {
            return await repository.GetAllUsers().ConfigureAwait(false);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = false;
            var status = string.Empty;
            var tokenValue = string.Empty;

            try
            {
                result = await repository.LoginUser(login).ConfigureAwait(false);
                if (result)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, login.UserId)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    login.Token = tokenHandler.WriteToken(token);
                    tokenValue = login.Token;
                    System.Console.WriteLine("token: " + tokenValue);
                }

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error On CreateNewCar controller endpoing. " + e.StackTrace);
                status = e.StackTrace;
            }
            return Json(new { Success = result, Token = tokenValue, Status = status });
        }

    }
}