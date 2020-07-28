using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace InMemoryVsSQLiteDemo.Test
{
    public class ConnectionFactory : IDisposable
    {

       // #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public EmployeeDBContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<EmployeeDBContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new EmployeeDBContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public EmployeeDBContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<EmployeeDBContext>().UseSqlite(connection).Options;

            var context = new EmployeeDBContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
       // #endregion
    }
}
