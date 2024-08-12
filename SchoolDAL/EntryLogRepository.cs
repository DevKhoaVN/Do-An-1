using MySql.Data.MySqlClient;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolDAL
{
    internal class EntryLogRepository : BaseRposiorty
    {
        public EntryLogRepository(): base()
        {

        }


        // Chèn log

        public bool InsertEntryLog(Entrylog log)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open(); // Mở kết nối đồng bộ

                    string query = @"
            INSERT INTO Entrylog (LogTime, StudentId, Status)
            VALUES (@LogTime, @StudentId, @Status)";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh
                        cmd.Parameters.AddWithValue("@LogTime", log.LogTime);
                        cmd.Parameters.AddWithValue("@StudentId", log.StudentId);
                        cmd.Parameters.AddWithValue("@Status", log.Status);

                        // Thực hiện câu lệnh đồng bộ
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được chèn hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                AnsiConsole.Markup($"[red]Lỗi khi chèn bản ghi:[/] {ex.Message}");
                return false;
            }
        }


        // Hàm trả về tất cả entrylog theo id
        public List<Entrylog> GetEntryLogId(int id)
        {
            List<Entrylog> EntryLogs = new List<Entrylog>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    //Lệnh truy vấn
                    string query = @"
                    SELECT s.StudentId , 
                        s.Name, 
                        s.Class, 
                        e.LogTime, 
                        e.Status 
                    FROM entrylog as e 
                    INNER JOIN student as s ON e.StudentId = s.StudentId 
                    WHERE s.ParentId = @id 
                    ORDER BY e.LogTime DESC";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),
                                    Student = new Student
                                    {
                                        
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                    },
                                    LogTime = reader.GetDateTime("LogTime"),
                                    Status = reader.GetString("Status")
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return EntryLogs;
        }

        //Hàm trả về tất cả entrylog 
        public List<Entrylog> GetEntryLogAll()
        {
            List<Entrylog> EntryLogs = new List<Entrylog>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    //Lệnh truy vấn
                    string query = @"
                    SELECT s.StudentId ,
                        s.Name, 
                        s.Class, 
                        e.LogTime, 
                        e.Status 
                    FROM entrylog as e 
                    INNER JOIN student as s ON e.StudentId = s.StudentId 
                    ORDER BY e.LogTime DESC";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),

                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                    },
                                    LogTime = reader.GetDateTime("LogTime"),
                                    Status = reader.GetString("Status")
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return EntryLogs;
        }

        // Hàm trả về tất cả entrylog theo thời gian
        public List<Entrylog> GetEntryLogRangeTime(DateTime timeStart ,DateTime timeEnd)
        {
            List<Entrylog> EntryLogs = new List<Entrylog>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    // Tạo truy vấn
                    string query = "SELECT s.StudentId , s.Name , s.Class , e.LogTime , e.Status FROM entrylog as e inner join student as s on e.StudentId = s.StudentId where e.LogTime >= @timeStart && e.LogTime <= @timeEnd  order by e.LogTime DESC";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@timeStart", timeStart);
                        cmd.Parameters.AddWithValue("@timeEnd", timeEnd);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),

                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                    },
                                    LogTime = reader.GetDateTime("LogTime"),
                                    Status = reader.GetString("Status")
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return EntryLogs;
        }

        // Hàm trả về học sinh theo range time cho phụ huynh 
        public List<Entrylog> GetEntryLogRangeTimeForParent(DateTime timeStart, DateTime timeEnd , int id)
        {
            List<Entrylog> EntryLogs = new List<Entrylog>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    // Tạo truy vấn
                    string query = "SELECT s.StudentId , s.Name , s.Class , e.LogTime , e.Status FROM entrylog as e inner join student as s on e.StudentId = s.StudentId where e.LogTime >= @timeStart and e.LogTime <= @timeEnd and e.StudentId = @id order by e.LogTime DESC";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@timeStart", timeStart);
                        cmd.Parameters.AddWithValue("@timeEnd", timeEnd);
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),

                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                    },
                                    LogTime = reader.GetDateTime("LogTime"),
                                    Status = reader.GetString("Status")
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return EntryLogs;
        }
        public async Task<List<Entrylog>> GetEntryLogTodayAsync()
        {
            DateTime today = DateTime.Now.Date; // Get only the date part
            List<Entrylog> EntryLogs = new List<Entrylog>();

            try
            {
                using (var connect = GetConnection())
                {
                    await connect.OpenAsync();

                    // Lệnh truy vấn
                    string query = "SELECT s.StudentId, s.Name, s.Class, e.LogTime, e.Status " +
                                   "FROM entrylog AS e " +
                                   "INNER JOIN student AS s ON e.StudentId = s.StudentId " +
                                   "WHERE DATE(e.LogTime) = @today " +
                                   "ORDER BY e.LogTime DESC";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@today", today);

                        // Thực hiện truy vấn
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (await reader.ReadAsync())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),
                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                    },
                                    LogTime = reader.GetDateTime("LogTime"),
                                    Status = reader.GetString("Status")
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }

            return EntryLogs;
        }


        public List<Entrylog> GetEntryLogToAlert()
        {
            List<Entrylog> EntryLogs = new List<Entrylog>();
            DateTime today = DateTime.Now;

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    //Lệnh truy vấn
                    string query = "SELECT s.StudentId, s.Name, StatusCounts.StatusCount FROM ( SELECT StudentId, COUNT(Status) AS StatusCount FROM EntryLog GROUP BY StudentId ) AS StatusCounts INNER JOIN Student s ON StatusCounts.StudentId = s.StudentId WHERE StatusCounts.StatusCount % 2 = 1;";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var log = new Entrylog
                                {
                                    StudentId = reader.GetInt32("StudentId"),

                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name")
                                    },
                                };

                                // Thêm đối tượng Entrylog vào danh sách
                                EntryLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return EntryLogs;
        }
    }
}
