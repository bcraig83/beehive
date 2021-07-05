using System.Threading.Tasks;
using Beehive.Api.Core.Models.Domain;

namespace Beehive.Api.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrumDbContext _context;

        public UnitOfWork(
            DrumDbContext context,
            IRepository<Drum> drumRepository)
        {
            _context = context;
            DrumRepository = drumRepository;
        }

        public IRepository<Drum> DrumRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}