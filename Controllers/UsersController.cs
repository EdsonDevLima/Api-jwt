using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Config;
using MyApi.Models;
using MyApi.Services;
using MyApi.Enum;
using Microsoft.AspNetCore.Identity;

namespace MyApi.Controllers{
    [ApiController]
    public class UsersController:ControllerBase{
        private readonly IAuthInterface authServices;
        private readonly ContextApplication context;
        public UsersController(IAuthInterface _authServices,ContextApplication _context){
            authServices = _authServices;
            context = _context;
        }





        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegister dataInput){
            var user = await context.Users.FirstOrDefaultAsync(userDb => userDb.Email == dataInput.Email || userDb.Name == dataInput.Name);
            if(user != null)
            {
                return Unauthorized(new {messages = "Email/usuario ja esta sendo cadastrado"});
            }


            try{
            authServices.CriptPass(dataInput.Password,out byte[] passHash, out byte[] passSalt);
            var newUser = new Users
            {
                Name = dataInput.Name,
                Email = dataInput.Email,
                PasswordSalt = passSalt,
                PasswordHash = passHash,
                Role = Roles.User,
                Age = dataInput.Age

            };   
            await context.Users.AddAsync(newUser);
            var token = authServices.CreateToken(newUser);
            return Ok(new {message = "usuario criado com sucesso",token = token});
            }catch(Exception e){
                return BadRequest(new {messageError = e.Message});
            }
            
             
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLogin dataLogin){
            
            var user = await context.Users.FirstOrDefaultAsync(userDb => userDb.Email == dataLogin.Email);
            if(user == null){
                return Unauthorized(new {message ="Email nao cadastrado"});
            }
            try{
                var token = authServices.CreateToken(user);
                return Ok(new {message = "usuario autenticado",token});


            }   catch(Exception e){
                var result = new ObjectResult("Internal Server Error");
                result.StatusCode = 500;
                return result;
            }
            
        }
        
    }
}