using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.Films;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
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
            Duration = film.Duration,
            Date = film.Date,
            Director = film.Director,
            ExistingPoster = film.Poster,
            Genre = film.Genre,
            Name = film.Name
        };
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ms = new MemoryStream();
        await FilmModel.File.CopyToAsync(ms);

        var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == FilmModel.Id);
        
        film.Name = FilmModel.Name;
        film.Date = FilmModel.Date;
        film.Genre = FilmModel.Genre;
        film.Director = FilmModel.Director;
        film.Poster = ms.ToArray();
        film.Duration = FilmModel.Duration;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}