using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.AccountModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email {get;set;}
        [Required(ErrorMessage = "Не указан login")]
        public string Login { get;set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Пароль введён не верно")]
        public string ConfirmPassword {get;set;}
    }
}