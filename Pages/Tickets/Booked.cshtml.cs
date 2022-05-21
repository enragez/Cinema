using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.Tickets;

public class BookedModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public BookedModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<TicketModel> Tickets { get; set; }

    public async Task OnGetAsync()
    {
        Tickets = await _context.Tickets
            .Include(t =>t.User)
            .Include(t => t.FilmSession)
            .ThenInclude(fs => fs.Film)
            .Where(t => !t.Paid)
            .OrderByDescending(t => t.FilmSession.Date)
            .Select(t => new TicketModel
            {
                Id = t.Id,
                Column = t.Column,
                Date = t.FilmSession.Date,
                Film = t.FilmSession.Film.Name,
                Paid = t.Paid ? "Да" : "Нет",
                Price = t.FilmSession.Price,
                Hall = t.FilmSession.Hall,
                Row = t.Row,
                FilmSessionId = t.FilmSession.Id,
                FilmId = t.FilmSession.Film.Id,
                UserName = t.User.UserName
            })
            .ToListAsync();
    }
}