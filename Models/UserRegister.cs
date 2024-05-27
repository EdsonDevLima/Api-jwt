
using System.ComponentModel.DataAnnotations;
using MyApi.Enum;

namespace MyApi.Models{
    public class UserRegister{
    
        [Required(ErrorMessage ="Campo obrigatorio")]
        public string Name{get;set;}


        [Required(ErrorMessage ="Campo obrigatorio"),EmailAddress]        
        public string Email{get;set;}
        [Required(ErrorMessage ="Campo obrigatorio")]
        public int Age{get;set;}
        [Required(ErrorMessage ="Campo obrigatorio")]
        public Roles Role{get;set;}
        [Required(ErrorMessage ="Campo obrigatorio")]
        public string Password{get;set;}
        [Required(ErrorMessage ="Campo obrigatorio"),Compare("Password")]
        public string ConfirmPassword{get;set;}
    
    }
}