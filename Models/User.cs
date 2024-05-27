using System.ComponentModel.DataAnnotations;
using MyApi.Enum;
namespace MyApi.Models{
    public class Users{
        [Key]
        public int Id{get;set;}
        public string Name{get;set;}
        public string Email{get;set;}
        public int Age{get;set;}
        public Roles Role{get;set;}
        public byte[] PasswordHash{get;set;}
        public byte[] PasswordSalt{get;set;}
    
    }
}