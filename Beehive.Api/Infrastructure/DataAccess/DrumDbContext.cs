using Beehive.Api.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Api.Infrastructure.DataAccess
{
    public class DrumDbContext : DbContext
    {
        public DrumDbContext(DbContextOptions<DrumDbContext> options) : base(options)
        {
        }

        public DbSet<Drum> Drums { get; set; }
    }
}