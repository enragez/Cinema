namespace Cinema.Data.Entities;

public class FilmSession
{
    public int Id { get; set; }
    
    public Film Film { get; set; }
    
    public int Hall { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Price { get; set; }
}