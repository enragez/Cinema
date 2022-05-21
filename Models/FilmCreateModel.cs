using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class FilmCreateModel
{
    public int Id { get; set; }
    
    [Display(Name = "Имя")]
    public string Name { get; set; }
    
    [Display(Name = "Жанр")]
    public string Genre { get; set; }
    
    [Display(Name = "Режиссер")]
    public string Director { get; set; }
    
    [Display(Name = "Дата выхода")]
    public DateTime Date { get; set; }
    
    [Display(Name = "Длительность")]
    public int Duration { get; set; }
    
    [Display(Name = "Постер")]
    public IFormFile File { get; set; }
}