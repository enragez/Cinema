using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.MyTickets;

public class CancelBookModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public CancelBookModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public TicketModel Ticket { get; set; }

    public async Task OnGetAsync(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.User)
            .Include(t => t.FilmSession)
            .ThenInclude(fs => fs.Film)
            .FirstOrDefaultAsync(t => t.Id == id);

        Ticket = new TicketModel
        {
            Id = ticket.Id,
            Film = ticket.FilmSession.Film.Name,
            Date = ticket.FilmSession.Date,
            Column = ticket.Column,
            Hall = ticket.FilmSession.Hall,
            Paid = ticket.Paid ? "Да" : "Нет",
            Price = ticket.FilmSession.Price,
            Row = ticket.Row,
            FilmSessionId = ticket.FilmSession.Id,
            FilmId = ticket.FilmSession.Film.Id,
            UserName = ticket.User.UserName
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == Ticket.Id);

        _context.Tickets.Remove(ticket);

        await _context.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }
}