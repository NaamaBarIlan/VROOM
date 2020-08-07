using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VROOM.Data;

namespace XUnitTestProject1
{
    public abstract class DatabaseTest : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly VROOMDbContext _db;

        public DatabaseTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new VROOMDbContext(
                new DbContextOptionsBuilder<VROOMDbContext>()
                .UseSqlite(_connection)
                .Options);

            _db.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
