using Cinema.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data;

public sealed class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Film> Films { get; set; } = null!;

    public DbSet<FilmSession> FilmSessions { get; set; } = null!;
    
    public DbSet<Ticket> Tickets { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}
