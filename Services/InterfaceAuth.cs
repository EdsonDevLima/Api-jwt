using System.Security.Claims;
using MyApi.Models;

namespace MyApi.Services{
    public interface IAuthInterface{
        void CriptPass(string password,out byte[] passHash,out byte[] passSalt);
        bool VerifyPassword(string password,byte[] passHash,byte[] passSalt);
        string CreateToken(Users dataInput);
        ClaimsPrincipal VerifyToken(string Token);
    }

}