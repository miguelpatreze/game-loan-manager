using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task InsertOneAsync(T entity, CancellationToken cancellationToken = default);

    }
}
