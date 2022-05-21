using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.FilmSessions;

public class ViewModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ViewModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<FilmSession> FilmSessions { get; set; }
    
    public FilmModel Film { get; set; }

    public async Task OnGetAsync(int? id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);

        Film = new FilmModel
        {
            Id = film.Id,
            Name = film.Name,
            Date = film.Date,
            Director = film.Director,
            Duration = film.Duration,
            Genre = film.Genre,
            Poster = film.Poster
        };
        
        FilmSessions = await _context.FilmSessions
            .Where(fs => fs.Film.Id == id && fs.Date >= DateTime.Now)
            .Select(u => new FilmSession
            {
                Film = u.Film.Name,
                Id = u.Id,
                Date = u.Date,
                Price = u.Price,
                Hall = u.Hall
            })
            .OrderBy(u => u.Date)
            .ToListAsync();
    }
}