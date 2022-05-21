using Cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmSession = Cinema.Models.FilmSession;

namespace Cinema.Pages.Tickets;

public class IssueModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public IssueModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public FilmSession FilmSession { get; set; }
    
    [BindProperty]
    public int? TicketId { get; set; }
    
    [BindProperty]
    public int Row { get; set; }
    
    [BindProperty]
    public int Column { get; set; }
    
    [BindProperty]
    public string UserName { get; set; }

    public async Task OnGetAsync(int ticketId)
    {
        var ticket = await _context.Tickets
            .Include(t => t.User)
            .Include(t => t.FilmSession)
            .ThenInclude(fs => fs.Film)
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        FilmSession = new FilmSession
        {
            Id = ticket.FilmSession.Id,
            Film = ticket.FilmSession.Film.Name,
            Date = ticket.FilmSession.Date,
            Hall = ticket.FilmSession.Hall,
            Price = ticket.FilmSession.Price,
        };
        
        TicketId = ticketId;
        Row = ticket.Row;
        Column = ticket.Column;
        UserName = ticket.User.UserName;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == TicketId.Value);

        ticket.Paid = true;
        
        await _context.SaveChangesAsync();
        
        return RedirectToPage("./Buy", new
        {
            id = FilmSession.Id
        });
    }
}