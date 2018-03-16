using System;
using Autofac;
using AzureStorage;
using AzureStorage.Tables;
using Common;
using Common.Log;
using JetBrains.Annotations;
using Lykke.SettingsReader;

namespace Lykke.Logging
{
    public static class AutofacExtension
    {
        /// <summary>
        /// Registers <see cref="IFixLogEntityRepository"/>/>
        /// </summary>
        /// <param name="containerBuilder">A container builder</param>
        /// <param name="connectionString">A connection string for an azure table</param>
        /// <param name="tableName">A name of the table</param>
        public static void RegisterFixLogEntityRepository([NotNull] this ContainerBuilder containerBuilder, [NotNull] IReloadingManager<string> connectionString, [NotNull] string tableName)
        {
            if (containerBuilder == null)
            {
                throw new ArgumentNullException(nameof(containerBuilder));
            }

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (tableName == null)
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            containerBuilder.Register(c => AzureTableStorage<FixLogEntity>.Create(connectionString, tableName, c.Resolve<ILog>()))
                .As<INoSQLTableStorage<FixLogEntity>>()
                .SingleInstance();

            containerBuilder.RegisterType<FixLogEntityRepository>()
                .As<IFixLogEntityRepository>()
                .As<IStopable>()
                .SingleInstance();
        }
    }
}
