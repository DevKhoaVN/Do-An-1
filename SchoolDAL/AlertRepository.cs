using MySql.Data.MySqlClient;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolDAL
{
    internal class AlertRepository: BaseRposiorty
    {
        public AlertRepository() : base()
        {

        }

        public bool InsertAlert(int studentId, DateTime alertTime)
        {
            // Câu lệnh SQL để chèn dữ liệu vào bảng Alert
            string query = "INSERT INTO Alert (StudentId, AlertTime) VALUES (@StudentId, @AlertTime)";

            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (MySqlConnection connection = GetConnection())
                {
                    // Mở kết nối đến cơ sở dữ liệu
                    connection.Open();

                    // Bắt đầu một giao dịch
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        // Tạo đối tượng MySqlCommand để thực thi câu lệnh SQL trong giao dịch
                        using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                        {
                            // Thêm các tham số vào câu lệnh SQL
                            command.Parameters.AddWithValue("@StudentId", studentId);
                            command.Parameters.AddWithValue("@AlertTime", alertTime);

                            // Thực thi câu lệnh SQL
                            int result = command.ExecuteNonQuery();

                            // Kiểm tra số lượng hàng bị ảnh hưởng (1 nếu thành công, 0 nếu thất bại)
                            if (result > 0)
                            {
                                // Xác nhận giao dịch
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                // Hoàn tác giao dịch nếu không có bản ghi nào bị ảnh hưởng
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có (ghi log, thông báo lỗi, vv.)
                Console.WriteLine($"Lỗi: {ex.Message}");
                return false;
            }
        }
        //Hàm trả về tất cả entrylog 
        public List<Alert> GetAlert()
        {
            List<Alert> Alerts = new List<Alert>();
            DateTime today = DateTime.Now;

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();

                    //Lệnh truy vấn
                    string query = " select s.Name  , s.Class, p.ParentName ,p.ParentPhone ,  p.ParentAddress , p.ParentEmail , a.AlertTime from alert as a inner join student as s on a.StudentId = s.StudentId inner join parent as p on s.StudentId = p.ParentId where a.AlertTime = @today ";
                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@today", today);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var alert = new Alert
                                {
                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                        Parent = new Parent
                                        {
                                            ParentName = reader.GetString("ParentName"),
                                            ParentPhone = reader.GetInt32("ParentPhone"),
                                            ParentEmail = reader.GetString("ParentEmail"),
                                            ParentAddress = reader.GetString("ParentAddress")
                                        }
                                    },

                                    AlertTime = reader.GetDateTime("AlertTime")                       
                                };

                                // Thêm đối tượng Alert vào danh sách
                                Alerts.Add(alert);
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
            return Alerts ;
        }

        // Hiển thị lịch sử cảnh báo từ mời -> cũ
        public List<Alert> GetAlertAll()
        {
            List<Alert> Alerts = new List<Alert>();
            DateTime today = DateTime.Now;

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();

                    //Lệnh truy vấn
                    string query = " select s.Name , s.Class, p.ParentName ,p.ParentPhone ,  p.ParentAddress , p.ParentEmail , a.AlertTime from alert as a inner join student as s on a.StudentId = s.StudentId inner join parent as p on s.StudentId = p.ParentId order by a.AlertTime desc";
                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@today", today);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Entrylog
                                var alert = new Alert
                                {
                                    Student = new Student
                                    {
                                        Name = reader.GetString("Name"),
                                        Class = reader.GetString("Class"),
                                        Parent = new Parent
                                        {
                                            ParentName = reader.GetString("ParentName"),
                                            ParentPhone = reader.GetInt32("ParentPhone"),
                                            ParentEmail = reader.GetString("ParentEmail"),
                                            ParentAddress = reader.GetString("ParentAddress")
                                        }
                                    },

                                    AlertTime = reader.GetDateTime("AlertTime")
                                };

                                // Thêm đối tượng Alert vào danh sách
                                Alerts.Add(alert);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red][ERROR]: Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
            }
            return Alerts;
        }


    }
}
