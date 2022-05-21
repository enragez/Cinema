using Microsoft.AspNetCore.Identity;

namespace Cinema.Data.Entities;

public class Ticket
{
    public int Id { get; set; }
    
    public FilmSession FilmSession { get; set; }
    
    public bool Paid { get; set; }
    
    public int Row { get; set; }
    
    public int Column { get; set; }
    
    public IdentityUser User { get; set; }
}