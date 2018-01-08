namespace QuickFix.Lykke
{
    /// <inheritdoc />
    public sealed class LykkeLogFactory : ILogFactory
    {
        private readonly Common.Log.ILog _lykkeLog;
        private readonly bool _logIncoming;
        private readonly bool _logOutgoing;
        private readonly bool _logEvent;

        public LykkeLogFactory(Common.Log.ILog lykkeLog, bool logIncoming = true, bool logOutgoing = true, bool logEvent = true)
        {
            this._lykkeLog = lykkeLog;
            _logIncoming = logIncoming;
            _logOutgoing = logOutgoing;
            _logEvent = logEvent;
        }

        /// <inheritdoc />
        public ILog Create(SessionID sessionId)
        {
            return new LykkeQuickFixLog(_lykkeLog, sessionId, _logIncoming, _logOutgoing, _logEvent);
        }
    }
}