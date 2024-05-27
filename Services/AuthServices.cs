using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using MyApi.Models;

namespace MyApi.Services{
    public class AuthServices:IAuthInterface{
        private readonly IConfiguration Config;
        public AuthServices(IConfiguration _Config){
            Config = _Config;
        }
        public void CriptPass(string password,out byte[] passHash,out byte[] passSalt){


                using(var hmac = new HMACSHA512()){
                    passSalt = hmac.Key;
                    passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }
        public bool VerifyPassword(string password,byte[] passHash,byte[] passSalt){
            
            using ( var hmac = new HMACSHA512(passSalt))
            {
                var pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass.SequenceEqual(passHash);
                
            }
        
        }
        public string CreateToken(Users dataInput){
            
            //dados que vao conter no token
            List<Claim> Clains = new List<Claim>{
                new Claim("Id",dataInput.Id.ToString()),
                new Claim("Name",dataInput.Name),
                new Claim("Email",dataInput.Email),
                new Claim("Role",dataInput.Role.ToString())
            };
            //config algoritimo de criptografia
            //key chave para geraçao do token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Config.GetSection("AppSettings:Key").Value));
            //as credenciais serve a assinatura digital do token, ele é usado para criaçao e verificaçao
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            //configuraçao do token - data de expiraçao e dados que vao conter no token
            var token = new JwtSecurityToken(claims:Clains,expires:DateTime.Now.AddDays(1),signingCredentials:credentials);
            //inscriçao do token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    public ClaimsPrincipal VerifyToken(string Token){
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Config.GetSection("AppSettings:Key").Value));
                    var ParamsOfValidation = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =key ,
                    ValidateAudience = false,
                    ValidateIssuer = false
                    };
                    // Cria uma instância do JwtSecurityTokenHandler para validar o token ou manipula-lo
                    var tokenHandler = new JwtSecurityTokenHandler();
                    try{
                        var verify = tokenHandler.ValidateToken(Token,ParamsOfValidation,out SecurityToken validatedToken);
                        return verify;


                        
                    }catch(Exception e){
                        Console.WriteLine($"Token validation failed: {e.Message}");
                        return null;

                    }



    }

    
        
    }
}