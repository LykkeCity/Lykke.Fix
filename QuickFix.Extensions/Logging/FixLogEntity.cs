using System;
using System.Threading;
using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;

namespace Lykke.QuickFix.Extensions.Logging
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal sealed class FixLogEntity : AzureTableEntity
    {
        public DateTime Time { get; set; }
        public string SenderCompId { get; set; }
        public string TargetCompId { get; set; }
        public string Message { get; set; }
        public FixMessageDirection Direction { get; set; }
        private static int _msgCounter;

        public FixLogEntity()
        {

        }

        public FixLogEntity(DateTime time, string senderCompId, string targetCompId, string message, FixMessageDirection direction)
        {
            Time = time;
            SenderCompId = senderCompId;
            TargetCompId = targetCompId;
            Message = message;
            Direction = direction;

            PartitionKey = targetCompId; // Always use Client's SenderId
            RowKey = time.ToString("s") + Interlocked.Increment(ref _msgCounter);
        }
    }
}
