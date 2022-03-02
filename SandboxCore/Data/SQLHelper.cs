﻿using Npgsql;
using System;
using System.Threading.Tasks;

namespace SandboxCore.Data
{
    /// <summary>
    /// Collections of methods used to operate on a database.
    /// </summary>
    public static class SQLHelper
    {
        /// <summary>
        /// Checks that a connection can be made to the database, and returns the connection string to be used to connect.
        /// </summary>
        /// <param name="server">Server to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        /// <param name="username">Username of account.</param>
        /// <param name="password">Password of account.</param>
        /// <param name="database">Name of database to connect to.</param>
        /// <returns>Returns the connection string and the connection state.</returns>
        public static Tuple<string, string> PostgreSQLConnection(string server, string port, string username, string password, string database)
        {
            var connectionString = $"Host={server};Port={port};Username={username};Password={password};Database={database}";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var connectionResult = Task.Run(async () =>
                {
                    try
                    {
                        await connection.OpenAsync();
                        return connection.State.ToString();
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                });
                return Tuple.Create(connectionResult.Result, connectionString);
            }
        }
    }
 }