using Business.Abstract;
using Core.Entity.Models;
using Core.Security.Hasing;
using Core.Security.TokenHandler;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthManager _authManager;
        private readonly TokenGenerator _tokenGenerator;
        private readonly HashingHandler _hashingHandler;
        private readonly IRoleManager _roleManager;
        public AuthController(IAuthManager authManager, TokenGenerator tokenGenerator, HashingHandler hashingHandler, IRoleManager roleManager)
        {
            _authManager = authManager;
            _tokenGenerator = tokenGenerator;
            _hashingHandler = hashingHandler;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<object> LoginUser(LoginDTO model)
        {
            var user = _authManager.Login(model.Email);

            if (user == null) return Ok(new {status = 404, message = "Bele bir istifadeci tapilmadi"});

            if (user.Email == model.Email && user.Password == _hashingHandler.PasswordHash(model.Password)) 
            {
                var role = _roleManager.GetRole(user.Id);
                var resultUser = new UserDTO(user.FullName, user.Email);
                resultUser.Token = _tokenGenerator.Token(user, role.Name);

                return Ok(new { status = 200, message = resultUser });


            }


                return Ok(new { status = 404, message = "Email ve ya sifre sefdi" });

        }   


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var password = model.Password;
            var user = _authManager.GetUserByEmail(model.Email);

            if (user != null)
            {
                return Ok("Email is exist.");
            }
            if (password.Length >= 5)
            {
                _authManager.Register(model);
                return Ok(new { status = 200, message = "Qeydiyyatdan keçdiniz." });
            }
                    
            else
            {
                return Ok(new { status = 404, message = "Şifrə minimum 5 simvoldan ibarət olmalıdır !" });
            }
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpGet("allusers")]
        public List<K205User> GetUsers()
        {
            return _authManager.GetUsers();
        }

        [HttpGet("GetUserByRole/{userId}")]
        public async Task<Role> GetUserByRole(int userId)
        {

            return _roleManager.GetRole(userId);
        }

    }
}
