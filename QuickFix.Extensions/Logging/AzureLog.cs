using System;
using QuickFix;

namespace Lykke.QuickFix.Extensions.Logging
{
    internal sealed class AzureLog : ILog
    {
        private readonly IFixLogEntityRepository _entityRepository;
        private readonly SessionID _sessionID;

        public AzureLog(IFixLogEntityRepository entityRepository, SessionID sessionID)
        {
            _entityRepository = entityRepository;
            _sessionID = sessionID;
        }
        public void Dispose()
        {
            // Nothing to do here
        }

        public void Clear()
        {
            // Not applicable
        }

        public void OnIncoming(string msg)
        {
            _entityRepository.WriteLogItem(DateTime.UtcNow, _sessionID.SenderCompID, _sessionID.TargetCompID, msg.Replace("\u0001","|"), FixMessageDirection.Incoming);
        }

        public void OnOutgoing(string msg)
        {
            _entityRepository.WriteLogItem(DateTime.UtcNow, _sessionID.SenderCompID, _sessionID.TargetCompID, msg.Replace("\u0001","|"), FixMessageDirection.Outgoing);
        }

        public void OnEvent(string s)
        {
            // Log to the normal log
        }
    }
}
