using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.FilmSessions;

public class BookModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public BookModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public FilmSession FilmSession { get; set; }
    
    public List<string> ReservedSeats { get; set; }
    
    public List<string> BoughtSeats { get; set; }

    public async Task OnGetAsync(int? id)
    {
        var filmSession = await _context.FilmSessions
            .Include(fs => fs.Film)
            .FirstOrDefaultAsync(f => f.Id == id);

        FilmSession = new FilmSession
        {
            Id = filmSession.Id,
            Film = filmSession.Film.Name
        };

        var fsTickets = await _context.Tickets.Where(t => t.FilmSession.Id == filmSession.Id).ToListAsync();
        ReservedSeats = fsTickets.Where(t => !t.Paid).Select(t => $"{t.Row}:{t.Column}").ToList();
        BoughtSeats = fsTickets.Where(t => t.Paid).Select(t => $"{t.Row}:{t.Column}").ToList();
    }
}