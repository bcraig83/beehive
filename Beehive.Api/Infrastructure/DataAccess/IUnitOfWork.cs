using System.Threading.Tasks;
using Beehive.Api.Core.Models.Domain;

namespace Beehive.Api.Infrastructure.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Drum> DrumRepository { get; }
        Task<int> SaveChangesAsync();
    }
}