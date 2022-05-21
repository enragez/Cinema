using System.Collections.Immutable;
using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmSession = Cinema.Data.Entities.FilmSession;

namespace Cinema.Pages.FilmSessions;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public FilmSessionCreateModel FilmSessionModel { get; set; }
    
    public List<string> Films { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Films = await _context.Films.Select(f => f.Name).ToListAsync();

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var film = await _context.Films.FirstOrDefaultAsync(f => f.Name == FilmSessionModel.Film);
        
        var filmSession = new FilmSession
        {
            Film = film,
            Date = FilmSessionModel.Date,
            Hall = FilmSessionModel.Hall,
            Price = FilmSessionModel.Price
        };

        await _context.FilmSessions.AddAsync(filmSession);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}