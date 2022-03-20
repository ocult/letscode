using Microsoft.EntityFrameworkCore;

namespace LetsCode.Models;

public class CardContext : DbContext
{
    public CardContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Card>? Cards { get; set; } = null;
}
