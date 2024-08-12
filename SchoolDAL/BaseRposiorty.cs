using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace EntryLogManagement.SchoolDAL
{
    public class BaseRposiorty
    {
        protected readonly string ConnectionString = "Server=localhost;Database=entrylogmanagement;Uid=root;Pwd=Vakhoa205!";

        // Constructor nhận vào chuỗi kết nối
        public BaseRposiorty()
        {
            TestConnection();
        }

        // Phương thức lấy SqlConnection
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Phương thức kiểm tra kết nối
        protected void TestConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                }



            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Lỗi kết nối đến databasee: [/] {ex.Message}");

            }

        }
    }
}

