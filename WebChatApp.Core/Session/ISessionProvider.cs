using System;

namespace WebChatApp.Core.Session
{
    public interface ISessionProvider : IDisposable
    {
        ISession CurrentSession { get; }
    }
}
