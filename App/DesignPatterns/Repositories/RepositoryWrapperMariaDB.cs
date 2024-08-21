using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project.App.Databases;
using Project.App.Mqtt;

using System;
using System.Threading.Tasks;

namespace Project.App.DesignPatterns.Reponsitories
{
    public partial interface IRepositoryWrapperMariaDB
    {
        IRepositoryBaseMariaDB<MqttClient> MqttClients { get; }

        void SaveChanges();
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        MariaDBContext GetDBContext();
    }
    public partial class RepositoryWrapperMariaDB : IRepositoryWrapperMariaDB
    {
        private readonly MariaDBContext DbContext;
        private readonly RepositoryBaseMariaDB<MqttClient> mqttClients;


        public RepositoryWrapperMariaDB(MariaDBContext dbContext)
        {
            DbContext = dbContext;
        }
        public IRepositoryBaseMariaDB<MqttClient> MqttClients => mqttClients ?? new RepositoryBaseMariaDB<MqttClient>(DbContext);

        public void SaveChanges()
        {
            using (var transactionResult = DbContext.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot))
            {
                try
                {
                    DbContext.SaveChanges();
                    transactionResult.Commit();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("SaveChanges: " + ex.GetBaseException());
                    transactionResult.Rollback();
                    throw;
                }
            }

        }

        public async Task SaveChangesAsync()
        {
            using (var transactionResult = await DbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Snapshot))
            {
                try
                {
                    await DbContext.SaveChangesAsync();
                    await transactionResult.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("SaveChangesAsync: " + ex.GetBaseException());
                    await transactionResult.RollbackAsync();
                    throw;
                }
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

        public MariaDBContext GetDBContext()
        {
            return DbContext;
        }
    }
}
