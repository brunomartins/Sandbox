using Autodesk.DesignScript.Runtime;
using SandboxCore.Data;
using System.Collections;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public static class PostgreSQL
    {
        /// <summary>
        /// Checks that a connection can be made to the database, and returns the connection string to be used to connect.
        /// </summary>
        /// <param name="server">Server to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        /// <param name="username">Username of account.</param>
        /// <param name="password">Password of account.</param>
        /// <param name="database">Name of database to connect to.</param>
        /// <returns name="dbStatus">Notifies of the connection state.</returns>
        /// <returns name="CnnString">Provides the connection string for further operations.</returns>
        /// <search>database, sql, postgresql</search>
        [MultiReturn(new[] { "dbStatus", "CnnString" })]
        public static IDictionary Connect(string server, string username, string password, string database, string port = "5432")
        {
            var serverResults = SQLHelper.PostgreSQLConnection(server, port, username, password, database);

            return new Dictionary<string, string>
            {
                {"dbStatus", serverResults.Item1},
                {"CnnString", serverResults.Item2}
            };
        }
    }
}
