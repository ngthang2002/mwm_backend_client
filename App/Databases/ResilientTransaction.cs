using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.App.Databases
{
    public class ResilientTransaction
    {
        private DbContext _context;
        private ResilientTransaction(DbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public static ResilientTransaction New(DbContext context) =>
            new ResilientTransaction(context);

        public async Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                await action();
                await transaction.CommitAsync();
            });
        }


        public void Execute(Action action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = _context.Database.BeginTransaction();
                action();
                transaction.Commit();
            });
        }
    }
}
