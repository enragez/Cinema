namespace Cinema.Models;

public class TicketModel
{
    public int Id { get; set; }
    
    public string Film { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Hall { get; set; }
    
    public string Paid { get; set; }
    
    public int Price { get; set; }
    
    public int Row { get; set; }
    
    public int Column { get; set; }
    
    public int FilmSessionId { get; set; }
    
    public int FilmId { get; set; }
    
    public string UserName { get; set; }
}