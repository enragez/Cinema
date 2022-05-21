using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.Films;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<FilmModel> Films { get; set; }

    public async Task OnGetAsync()
    {
        Films = await _context.Films
            .Select(u => new FilmModel
            {
                Name = u.Name,
                Id = u.Id,
                Director = u.Director,
                Date = u.Date,
                Duration = u.Duration,
                Poster = u.Poster,
                Genre = u.Genre
            })
            .ToListAsync();
    }
}