using System.ComponentModel.DataAnnotations;
namespace MyApi.Models{
public class UserLogin{
    [Required(ErrorMessage ="Campo de email vazio"),EmailAddress(ErrorMessage ="Email Invalido")]
   public string Email{get;set;}
   [Required(ErrorMessage ="Campo de senha vazio")]
   public string Password{get;set;}
}}