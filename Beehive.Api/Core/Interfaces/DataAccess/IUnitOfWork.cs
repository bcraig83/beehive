using System.Threading.Tasks;
using Beehive.Api.Core.Models;

namespace Beehive.Api.Core.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Drum> DrumRepository { get; }

        Task<int> SaveChangesAsync();
    }
}