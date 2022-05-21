using Cinema.Data;
using Cinema.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmSession = Cinema.Models.FilmSession;

namespace Cinema.Pages.FilmSessions;

public class BookConfirmModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public BookConfirmModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public FilmSession FilmSession { get; set; }
    
    [BindProperty]
    public int Row { get; set; }
    
    [BindProperty]
    public int Column { get; set; }

    public async Task OnGetAsync(int id, int row, int column)
    {
        var filmSession = await _context.FilmSessions
            .Include(fs => fs.Film)
            .FirstOrDefaultAsync(f => f.Id == id);

        FilmSession = new FilmSession
        {
            Id = filmSession.Id,
            Film = filmSession.Film.Name,
            Date = filmSession.Date,
            Hall = filmSession.Hall,
            Price = filmSession.Price
        };
        
        Row = row;
        Column = column;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var filmSession = await _context.FilmSessions.FirstOrDefaultAsync(fs => fs.Id == FilmSession.Id);

        var userIdentity = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
        
        var ticket = new Ticket
        {
            Column = Column,
            Row = Row,
            FilmSession = filmSession,
            Paid = false,
            User = userIdentity
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("./Book", new
        {
            id = FilmSession.Id
        });
    }
}