using Microsoft.Data.Sqlite;

namespace CleanArchitecture.Infrastructure.DataContext {
    public class SqlLiteConnectionFactory {
        private SqliteConnection _connection;

        public SqliteConnection Create(string connectionString) {
            if (connectionString.ToLower().Contains(":memory:")) {
                if (_connection == null) {
                    _connection = new SqliteConnection(connectionString);
                    _connection.Open();
                }
                return _connection;
            }
            return new SqliteConnection(connectionString);
        }
    }
}
