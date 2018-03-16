using QuickFix;

namespace Lykke.Logging
{
    internal sealed class LykkeLog : ILog
    {
        private readonly global::Common.Log.ILog _lykkeLog;
        private readonly SessionID _sessionId;
        private readonly bool _logIncoming;
        private readonly bool _logOutgoing;
        private readonly bool _logEvents;

        public LykkeLog(global::Common.Log.ILog lykkeLog, SessionID sessionId, bool logIncoming, bool logOutgoing, bool logEvents)
        {
            _lykkeLog = lykkeLog;
            _sessionId = sessionId;
            _logIncoming = logIncoming;
            _logOutgoing = logOutgoing;
            _logEvents = logEvents;
        }

        public void Dispose()
        {
        }

        public void Clear()
        {
        }

        public void OnIncoming(string msg)
        {
            if (_logIncoming)
            {
                _lykkeLog.WriteInfoAsync(nameof(LykkeLog), nameof(OnIncoming), _sessionId.ToString(), msg.Replace("\u0001","|")).Wait();
            }
        }

        public void OnOutgoing(string msg)
        {
            if (_logOutgoing)
            {
                _lykkeLog.WriteInfoAsync(nameof(LykkeLog), nameof(OnOutgoing), _sessionId.ToString(), msg.Replace("\u0001","|")).Wait();
            }
        }

        public void OnEvent(string s)
        {
            if (_logEvents)
            {
                _lykkeLog.WriteInfoAsync(nameof(LykkeLog), nameof(OnEvent), _sessionId.ToString(), s).Wait();
            }
        }
    }
}
