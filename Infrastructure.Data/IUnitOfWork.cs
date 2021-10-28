using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancelationToken);
    }
}
