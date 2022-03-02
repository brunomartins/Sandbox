using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxCore.Data
{
    public static class SQLConnector
    {
        public static Tuple<string, string> PostgresConnect(string server, string port, string uid, string pw, string db)
        {
            var cs = $"Host={server};Port={port};Username={uid};Password={pw};Database={db}";

            var con = new NpgsqlConnection(cs);
            con.Open();
            var result = Tuple.Create(con.State.ToString(), cs);
            return result;
        }

    }
 
}
