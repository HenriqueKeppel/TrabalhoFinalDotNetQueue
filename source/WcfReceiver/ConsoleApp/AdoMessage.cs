using Dapper;
using MySql.Data.MySqlClient;
using System;

namespace ConsoleApp
{
    public class AdoMessage
    {
        private static string _connectionString = "Server=localhost;Database=dotnetqueue;Uid=root;Pwd=";

        public static void Insert(string mensagem)
        {
            using (MySqlConnection _conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    string sql = string.Format("insert into message values ('{0}');", mensagem);
                    _conn.Open();
                    _conn.Query(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }
    }
}
