using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.FilmSessions;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<FilmSession> FilmSessions { get; set; }

    public async Task OnGetAsync()
    {
        FilmSessions = await _context.FilmSessions
            .Select(u => new FilmSession
            {
                Film = u.Film.Name,
                Id = u.Id,
                Date = u.Date,
                Price = u.Price,
                Hall = u.Hall
            })
            .ToListAsync();
    }
}