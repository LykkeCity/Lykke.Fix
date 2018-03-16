using System;
using Common;

namespace Lykke.Logging
{
    public interface IFixLogEntityRepository : IStopable
    {
        void WriteLogItem(DateTime time, string senderCompId, string targetCompId, string message, FixMessageDirection direction);
    }
}
