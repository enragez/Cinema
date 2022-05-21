using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinema.Pages.Films;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public FilmCreateModel FilmModel { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ms = new MemoryStream();
        await FilmModel.File.CopyToAsync(ms);

        var film = new Film
        {
            Name = FilmModel.Name,
            Date = FilmModel.Date,
            Genre = FilmModel.Genre,
            Director = FilmModel.Director,
            Poster = ms.ToArray(),
            Duration = FilmModel.Duration
        };

        await _context.Films.AddAsync(film);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}