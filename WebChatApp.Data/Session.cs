using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;

namespace WebChatApp.Data
{
    internal class Session : ISession
    {
        private readonly ApplicationContext _context;

        private bool _disposedValue = false;

        public Session(ApplicationContext context)
        {
            _context = context;
        }

        public IDbConnection DbConnection => _context.Database.GetDbConnection();

        public Guid Id { get; } = Guid.NewGuid();

        #region Methods
        public IQueryable<T> Query<T>(bool ignoreGlobalFilters = false)
            where T : class
        {
            return ignoreGlobalFilters
                ? _context.Set<T>().IgnoreQueryFilters()
                : _context.Set<T>();
        }

        public async Task AddEntityAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IBaseEntity
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

            await FlushAsync(cancellationToken);
        }

        public async Task RemoveEntityPhysical<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            _context.Set<TEntity>().Remove(entity);

            await FlushAsync();
        }

        public async Task UpdateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IBaseEntity
        {
            _context.Set<TEntity>().Update(entity);

            await FlushAsync(cancellationToken);
        }
        public async Task FlushAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
