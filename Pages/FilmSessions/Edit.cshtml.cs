using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmSession = Cinema.Data.Entities.FilmSession;

namespace Cinema.Pages.FilmSessions;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public FilmSessionEditModel FilmSessionModel { get; set; }
    
    public List<string> Films { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var filmSession = await _context.FilmSessions
            .Include(fs => fs.Film)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (filmSession == null)
        {
            return NotFound();
        }
        
        Films = await _context.Films.Select(f => f.Name).ToListAsync();

        FilmSessionModel = new FilmSessionEditModel
        {
            Id = filmSession.Id,
            Date = filmSession.Date,
            Film = filmSession.Film.Name,
            Hall = filmSession.Hall,
            Price = filmSession.Price
        };
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var film = await _context.Films.FirstOrDefaultAsync(f => f.Name == FilmSessionModel.Film);

        var filmSession = await _context.FilmSessions.FirstOrDefaultAsync(fs => fs.Id == FilmSessionModel.Id);

        filmSession.Film = film;
        filmSession.Date = FilmSessionModel.Date;
        filmSession.Hall = FilmSessionModel.Hall;
        filmSession.Price = FilmSessionModel.Price;

        await _context.SaveChangesAsync();

        Films = await _context.Films.Select(f => f.Name).ToListAsync();
        
        return RedirectToPage("./Index");
    }
}