using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class UserCreateModel
{
    [Display(Name = "Имя")]
    public string Name { get; set; }
    
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Display(Name = "Номер телефона")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [Display(Name = "Администратор")]
    public bool IsAdmin { get; set; }
    
    [Display(Name = "Кассир")]
    public bool IsCashier { get; set; }
    
    [Display(Name = "Зритель")]
    public bool IsViewer { get; set; }
}