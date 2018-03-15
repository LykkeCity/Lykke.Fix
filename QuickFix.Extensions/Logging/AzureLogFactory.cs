using QuickFix;

namespace Lykke.QuickFix.Extensions.Logging
{
    /// <inheritdoc />
    public sealed class AzureLogFactory : ILogFactory
    {
        private readonly IFixLogEntityRepository _fixLogEntity;

        /// <inheritdoc />
        public AzureLogFactory(IFixLogEntityRepository fixLogEntity)
        {
            _fixLogEntity = fixLogEntity;
        }

        /// <inheritdoc />
        public ILog Create(SessionID sessionID)
        {
            return new AzureLog(_fixLogEntity, sessionID);
        }
    }
}
