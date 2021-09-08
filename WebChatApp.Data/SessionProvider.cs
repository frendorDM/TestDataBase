using System;
using System.Collections.Generic;
using WebChatApp.Core.Session;

namespace WebChatApp.Data
{
    internal class SessionProvider : ISessionProvider
    {
        private readonly Func<ApplicationContext> _dbContextFactory;

        private bool _disposedValue = false;

        private ISession _currentSession;

        public SessionProvider(
            Func<ApplicationContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ISession CurrentSession => _currentSession ?? (_currentSession = CreateSession());

        protected IList<ISession> Sessions { get; } = new List<ISession>();

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
                    foreach (var session in Sessions)
                    {
                        session.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }

        private ISession CreateSession()
        {
            var context = _dbContextFactory();

            var result = new Session(context);

            Sessions.Add(result);

            return result;
        }
    }
}
