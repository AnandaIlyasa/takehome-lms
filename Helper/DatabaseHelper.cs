using Npgsql;

namespace Lms.Helper;

internal class DatabaseHelper
{
    const string host = "localhost";
    const string db = "lemes";
    const string username = "postgres";
    const string password = "postgres";
    const string connString = $"Host={host}; Database={db}; Username={username}; Password={password}";
    NpgsqlConnection? conn;

    public NpgsqlConnection GetConnection()
    {
        if (conn == null)
        {
            conn = new NpgsqlConnection(connString);
        }
        return conn;
    }
}
