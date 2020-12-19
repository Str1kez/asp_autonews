using System.ComponentModel.DataAnnotations;

namespace asp_autonews.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить?")]
        public bool Remember { get; set; }
    }
}