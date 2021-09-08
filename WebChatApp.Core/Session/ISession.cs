using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebChatApp.Models.Entities;

namespace WebChatApp.Core.Session
{
    public interface ISession : IDisposable
    {
        Guid Id { get; }

        IQueryable<T> Query<T>(bool ignoreGlobalFilters = false)
            where T : class;

        Task AddEntityAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IBaseEntity;

        Task RemoveEntityPhysical<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        Task UpdateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IBaseEntity;

        Task FlushAsync(CancellationToken cancellationToken = default);
    }
}
