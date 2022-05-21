using System.Globalization;
using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Pages.Report;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<ReportRowModel> Rows { get; set; }

    public async Task OnGetAsync()
    {
        var tickets = await _context.Tickets
            .Include(t => t.FilmSession)
            .Where(t => t.Paid)
            .ToListAsync();

        var todayTickets = tickets.Where(t => t.FilmSession.Date.Date == DateTime.Now.Date).ToList();
        
        var currentDateWeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        
        var currentWeekTickets = tickets.Where(t => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(t.FilmSession.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday) == currentDateWeekNumber).ToList();

        var currentMonthTickets = tickets.Where(t => t.FilmSession.Date.Month == DateTime.Now.Month).ToList();
        
        var currentYearTickets = tickets.Where(t => t.FilmSession.Date.Year == DateTime.Now.Year).ToList();

        Rows = new List<ReportRowModel>
        {
            new()
            {
                Period = "Сегодня",
                TicketsCount = todayTickets.Count,
                Income = todayTickets.Sum(t => t.FilmSession.Price)
            },
            new()
            {
                Period = "Текущая неделя",
                TicketsCount = currentWeekTickets.Count,
                Income = currentWeekTickets.Sum(t => t.FilmSession.Price)
            },
            new()
            {
                Period = "Текущий месяц",
                TicketsCount = currentMonthTickets.Count,
                Income = currentMonthTickets.Sum(t => t.FilmSession.Price)
            },
            new()
            {
                Period = "Текущий год",
                TicketsCount = currentYearTickets.Count,
                Income = currentYearTickets.Sum(t => t.FilmSession.Price)
            },
            new()
            {
                Period = "Всё время",
                TicketsCount = tickets.Count,
                Income = tickets.Sum(t => t.FilmSession.Price)
            }
        };
    }
}