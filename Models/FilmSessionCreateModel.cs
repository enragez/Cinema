using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class FilmSessionCreateModel
{
    public int Id { get; set; }
    
    [Display(Name = "Фильм")]
    public string Film { get; set; }
    
    [Display(Name = "Зал")]
    public int Hall { get; set; }
    
    [Display(Name = "Время")]
    public DateTime Date { get; set; }
    
    [Display(Name = "Цена")]
    public int Price { get; set; }
}