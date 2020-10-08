using System;

namespace Injection.Messaging
{
    public interface IMessenger
    {
        void SendMessage(string message);
    }
}