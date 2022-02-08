using Beehive.Api2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Api2.DataAccess;

public class DrumDbContext : DbContext
{
    public DrumDbContext(DbContextOptions<DrumDbContext> options) : base(options)
    {
    }

    public DbSet<Drum> Drums { get; set; }
}