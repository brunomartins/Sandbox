using Npgsql;

namespace SandboxCore.Data
{
    public static class SQLConnector
    {
        public static void SQLConnect(string server, string port, string uid, string pw, string db)
        {
            var cs = $"Host={server};Port={port};Username={uid};Password={pw};Database={db}";
            
            var con = new NpgsqlConnection(cs);
            con.Open();

        }
    }
}
