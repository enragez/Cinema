using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.FilmSessions;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public FilmSessionEditModel FilmSessionModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var film = await _context.FilmSessions
            .Include(fs => fs.Film)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (film == null)
        {
            return NotFound();
        }
        
        FilmSessionModel = new FilmSessionEditModel
        {
            Id = film.Id,
            Film = film.Film.Name,
            Date = film.Date
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var entity = await _context.FilmSessions.FirstOrDefaultAsync(m => m.Id == FilmSessionModel.Id);

        _context.FilmSessions.Remove(entity);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}