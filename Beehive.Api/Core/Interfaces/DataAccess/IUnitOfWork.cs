using System.Threading.Tasks;
using Beehive.Api.Core.Models;
using Beehive.Api.Infrastructure.DataAccess;

namespace Beehive.Api.Core.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Drum> DrumRepository { get; }
        Task<int> SaveChangesAsync();
    }
}