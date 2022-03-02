using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxCore.Data
{
    /// <summary>
    /// Collections of methods used to operate on a database.
    /// </summary>
    public static class SQLConnector
    {
        /// <summary>
        /// Checks that a connection can be made to the database, and returns the connection string to be used for queries and commands.
        /// </summary>
        /// <param name="server">Server to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        /// <param name="username">Username of account.</param>
        /// <param name="password">Password of account.</param>
        /// <param name="database">Name of database to connect to.</param>
        /// <returns>Returns a tuple in the format of (ConnectionStatus, ConnectionString).</returns>
        public static Tuple<string, string> PostgreSQL(string server, string port, string username, string password, string database)
        {
            var connectionString = $"Host={server};Port={port};Username={username};Password={password};Database={database}";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = Tuple.Create(connection.State.ToString(), connectionString);
                return result;
            }
        }

    }
 
}
