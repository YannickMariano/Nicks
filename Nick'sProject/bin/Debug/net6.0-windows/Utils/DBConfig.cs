using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotNetEnv;
namespace Nick_sProject.Utils
{
    public static class DbConfig
    {
        static DbConfig()
        {
            DotNetEnv.Env.Load();
            Console.WriteLine("DB_HOST = " + Env.GetString("DB_HOST"));

        }

        public static NpgsqlConnection GetConnection()
        {
            Console.WriteLine("DB_HOST = " + Env.GetString("DB_HOST"));

            string connectionString = $"Host={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_NAME")};Username={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASS")}";
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}