using System.ComponentModel.DataAnnotations;

namespace EventFinder.Models.Enums
{
    public enum RoleEnum
    {
        [Display(Name = "Администратор")]
        Admin=1,
        
        [Display(Name = "Пользователь")]
        User
    }
}