using Domain.Entities;
using SQLite;
using Microsoft.Extensions.Options;

namespace Repository
{
    public class DbContext
    {
        public SQLiteConnection Connection { get; }

        public DbContext(IOptions<DatabaseOptions> options)
        {
            string databasePath = options.Value.DatabasePath;
            Connection = new SQLiteConnection(databasePath);
            Connection.CreateTable<UserEntity>();
        }
    }
}
