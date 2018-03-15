using QuickFix;

namespace Lykke.QuickFix.Extensions.Logging
{
    /// <inheritdoc />
    public sealed class LykkeLogFactory : ILogFactory
    {
        private readonly global::Common.Log.ILog _lykkeLog;
        private readonly bool _logIncoming;
        private readonly bool _logOutgoing;
        private readonly bool _logEvent;

        public LykkeLogFactory(global::Common.Log.ILog lykkeLog, bool logIncoming = true, bool logOutgoing = true, bool logEvent = true)
        {
            this._lykkeLog = lykkeLog;
            _logIncoming = logIncoming;
            _logOutgoing = logOutgoing;
            _logEvent = logEvent;
        }

        /// <inheritdoc />
        public ILog Create(SessionID sessionId)
        {
            return new LykkeLog(_lykkeLog, sessionId, _logIncoming, _logOutgoing, _logEvent);
        }
    }
}