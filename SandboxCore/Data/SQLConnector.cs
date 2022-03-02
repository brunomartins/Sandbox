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
        /*
         * 
         * 
         * QUERY METHOD - WIP
        public static ICollection PostgresQuery(string cs, string query)
        {
            var con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader rdr = cmd.ExecuteReader();

            string[] output = new string[] { };
            while (rdr.Read())
            {
                output.Add(rdr.GetValue.ToString())

            }

        }
        */
    }
 
}
