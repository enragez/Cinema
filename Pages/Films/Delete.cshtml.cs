using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.Films;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public FilmEditModel FilmModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
        
        if (film == null)
        {
            return NotFound();
        }
        
        FilmModel = new FilmEditModel
        {
            Id = film.Id,
            Name = film.Name
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == FilmModel.Id);

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}