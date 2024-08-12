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
    internal class StudentRepository : BaseRposiorty
    {

        public StudentRepository() : base()
        {

        }

        public bool AddStudent(Student student)
        {
            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();

                    // Thêm phụ huynh và lấy ID phụ huynh
                    string queryParent = @"
                INSERT INTO Parent (ParentName, ParentEmail, ParentPhone, ParentAddress) 
                VALUES (@ParentName, @ParentEmail, @ParentPhone, @ParentAddress); 
                SELECT LAST_INSERT_ID();";

                    int parentId;

                    using (var cmd = new MySqlCommand(queryParent, connect))
                    {
                        cmd.Parameters.AddWithValue("@ParentName", student.Parent.ParentName);
                        cmd.Parameters.AddWithValue("@ParentEmail", student.Parent.ParentEmail);
                        cmd.Parameters.AddWithValue("@ParentPhone", student.Parent.ParentPhone);
                        cmd.Parameters.AddWithValue("@ParentAddress", student.Parent.ParentAddress);

                        // Thực hiện truy vấn và lấy ID phụ huynh
                        parentId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Thêm sinh viên với ID phụ huynh vừa lấy
                    string queryStudent = @"
                INSERT INTO Student (ParentId, Name, Gender, DayOfBirth, Class, Address, Phone, JoinDay) 
                VALUES (@ParentId, @Name, @Gender, @DayOfBirth, @Class, @Address, @Phone, @JoinDay);";

                    using (var cmd = new MySqlCommand(queryStudent, connect))
                    {
                        cmd.Parameters.AddWithValue("@ParentId", parentId);
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@DayOfBirth", student.DayOfBirth);
                        cmd.Parameters.AddWithValue("@Class", student.Class);
                        cmd.Parameters.AddWithValue("@Address", student.Address);
                        cmd.Parameters.AddWithValue("@Phone", student.Phone);
                        cmd.Parameters.AddWithValue("@JoinDay", student.JoinDay);

                        // Thực hiện truy vấn
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗithêm học sinh[/]");
                AnsiConsole.WriteLine();
                return false;
            }
        }


        // Xóa

        public bool DeleteStudent(int studentId)
        {
            var inforStudent = GetStudentId(studentId).FirstOrDefault();
            var parentid = inforStudent.ParentId;
     
            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();

                    // Xóa phụ huynh trước
                    string queryDeleteParent = "DELETE FROM Parent WHERE ParentId = @parentid";
                    using (var cmdDeleteParent = new MySqlCommand(queryDeleteParent, connect))
                    {
                        cmdDeleteParent.Parameters.AddWithValue("@StudentId", studentId);
                        cmdDeleteParent.Parameters.AddWithValue("@parentid", parentid);
                        cmdDeleteParent.ExecuteNonQuery();
                    }

                    // Xóa học sinh
                    string queryDeleteStudent = "DELETE FROM Student WHERE StudentId = @StudentId";
                    using (var cmdDeleteStudent = new MySqlCommand(queryDeleteStudent, connect))
                    {
                        cmdDeleteStudent.Parameters.AddWithValue("@StudentId", studentId);
                        cmdDeleteStudent.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi khi xóa học sinh");
                AnsiConsole.WriteLine();
                return false;
            }
        }

        // update

        public bool UpdateStudent(Student student, int studentId)
        {      
            
            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();

                    // Cập nhật phụ huynh trước
                    string queryUpdateParent = @"
                UPDATE Parent 
                SET ParentName = COALESCE(NULLIF(@ParentName, ''), ParentName), 
                    ParentEmail = COALESCE(NULLIF(@ParentEmail, ''), ParentEmail), 
                    ParentPhone = COALESCE(NULLIF(@ParentPhone, 0), ParentPhone), 
                    ParentAddress = COALESCE(NULLIF(@ParentAddress, ''), ParentAddress)
                WHERE ParentId = @ParentId";

                    using (var cmdUpdateParent = new MySqlCommand(queryUpdateParent, connect))
                    {
                        cmdUpdateParent.Parameters.AddWithValue("@ParentId", student.Parent.ParentId);
                        cmdUpdateParent.Parameters.AddWithValue("@ParentName", student.Parent.ParentName ?? (object)DBNull.Value);
                        cmdUpdateParent.Parameters.AddWithValue("@ParentEmail", student.Parent.ParentEmail ?? (object)DBNull.Value);
                        cmdUpdateParent.Parameters.AddWithValue("@ParentPhone", student.Parent.ParentPhone == 0 ? (object)DBNull.Value : student.Parent.ParentPhone);
                        cmdUpdateParent.Parameters.AddWithValue("@ParentAddress", student.Parent.ParentAddress ?? (object)DBNull.Value);
                        cmdUpdateParent.ExecuteNonQuery();
                    }

                    // Cập nhật học sinh sau
                    string queryUpdateStudent = @"
                UPDATE Student 
                SET ParentId = COALESCE(NULLIF(@ParentId, 0), ParentId), 
                    Name = COALESCE(NULLIF(@Name, ''), Name), 
                    Gender = COALESCE(NULLIF(@Gender, ''), Gender), 
                    DayOfBirth = COALESCE(NULLIF(@DayOfBirth, '0001-01-01'), DayOfBirth), 
                    Class = COALESCE(NULLIF(@Class, ''), Class), 
                    Address = COALESCE(NULLIF(@Address, ''), Address), 
                    Phone = COALESCE(NULLIF(@Phone, 0), Phone), 
                    JoinDay = COALESCE(NULLIF(@JoinDay, '0001-01-01'), JoinDay)
                WHERE StudentId = @StudentId";

                    using (var cmdUpdateStudent = new MySqlCommand(queryUpdateStudent, connect))
                    {
                        cmdUpdateStudent.Parameters.AddWithValue("@StudentId", studentId);
                        cmdUpdateStudent.Parameters.AddWithValue("@ParentId", student.ParentId == 0 ? (object)DBNull.Value : student.ParentId);
                        cmdUpdateStudent.Parameters.AddWithValue("@Name", student.Name ?? (object)DBNull.Value);
                        cmdUpdateStudent.Parameters.AddWithValue("@Gender", student.Gender ?? (object)DBNull.Value);
                        cmdUpdateStudent.Parameters.AddWithValue("@DayOfBirth", student.DayOfBirth == DateTime.MinValue ? (object)DBNull.Value : student.DayOfBirth);
                        cmdUpdateStudent.Parameters.AddWithValue("@Class", student.Class ?? (object)DBNull.Value);
                        cmdUpdateStudent.Parameters.AddWithValue("@Address", student.Address ?? (object)DBNull.Value);
                        cmdUpdateStudent.Parameters.AddWithValue("@Phone", student.Phone == 0 ? (object)DBNull.Value : student.Phone);
                        cmdUpdateStudent.Parameters.AddWithValue("@JoinDay", student.JoinDay == DateTime.MinValue ? (object)DBNull.Value : student.JoinDay);
                        cmdUpdateStudent.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red][ERROR]: Đã xảy ra lỗi:[/] {ex.Message}");
                AnsiConsole.WriteLine();
                return false;
            }
        }



        public List<Student> GetStudentId(int id)
        {
            List<Student> Students = new List<Student>();

            try
            {
                using (var connect = GetConnection())
                {
                    //Lệnh truy vấn
                    connect.Open();
                    string query = "select S.StudentId , S.Name , S.Gender, S.DayOfBirth , S.Class , S.Address , S.Phone , S.JoinDay , P.ParentName , P.ParentEmail , P.ParentPhone, P.ParentAddress from student as S INNER JOIN parent as P on S.ParentId = P.ParentId where S.StudentId = @id";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        // Tạo tham số
                        cmd.Parameters.AddWithValue("@id", id);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ kiệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Student
                                var student = new Student
                                {
                                    StudentId = reader.GetInt32("StudentId"),
                                    Name = reader.GetString("Name"),
                                    Gender = reader.GetString("Gender"),
                                    DayOfBirth = reader.GetDateTime("DayOfBirth"),
                                    Class = reader.GetString("Class"),
                                    Address = reader.GetString("Address"),
                                    Phone = reader.GetInt32("Phone"),
                                    JoinDay = reader.GetDateTime("JoinDay"),
                                    Parent = new Parent // Tạo đối tượng Parent
                                    {
                                        ParentName = reader.GetString("ParentName"),
                                        ParentEmail = reader.GetString("ParentEmail"),
                                        ParentPhone = reader.GetInt32("ParentPhone"),
                                        ParentAddress = reader.GetString("ParentAddress")
                                    }
                                };

                                // Thêm đối tượng Student vào danh sách
                                Students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi khi lấy học sinh theo id[/]");
                AnsiConsole.WriteLine();
            }

            return Students;
        }


        public List<Student> GetStudentAll()
        {
            List<Student> Students = new List<Student>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    //Lệnh truy vấn
                    string query = "select S.StudentId , S.Name , S.Gender, S.DayOfBirth , S.Class , S.Address , S.Phone , S.JoinDay , P.ParentName , P.ParentEmail , P.ParentPhone, P.ParentAddress from student as S INNER JOIN parent as P on S.ParentId = P.ParentId";

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ kiệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Student
                                var student = new Student
                                {
                                    StudentId = reader.GetInt32("StudentId"),
                                    Name = reader.GetString("Name"),
                                    Gender = reader.GetString("Gender"),
                                    DayOfBirth = reader.GetDateTime("DayOfBirth"),
                                    Class = reader.GetString("Class"),
                                    Address = reader.GetString("Address"),
                                    Phone = reader.GetInt32("Phone"),
                                    JoinDay = reader.GetDateTime("JoinDay"),
                                    Parent = new Parent // Tạo đối tượng Parent
                                    {
                                        ParentName = reader.GetString("ParentName"),
                                        ParentEmail = reader.GetString("ParentEmail"),
                                        ParentPhone = reader.GetInt32("ParentPhone"),
                                        ParentAddress = reader.GetString("ParentAddress")
                                    }
                                };

                                // Thêm đối tượng Student vào danh sách
                                Students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red][ERROR]: Đã xảy ra lỗi:[/]");
                AnsiConsole.WriteLine();
            }

            return Students;
        }


        public List<Student> GetStudentByRangeTime(DateTime timeStart, DateTime timeEnd)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (var connect = GetConnection())
                {
                    connect.Open();
                    // Lệnh truy vấn
                    string query = @"
                SELECT S.StudentId, S.Name, S.Gender, S.DayOfBirth, S.Class, S.Address, S.Phone, S.JoinDay, 
                       P.ParentName, P.ParentEmail, P.ParentPhone, P.ParentAddress
                FROM student AS S
                INNER JOIN parent AS P ON S.ParentId = P.ParentId
                WHERE S.JoinDay BETWEEN @timeStart AND @timeEnd"; 

                    // Tạo command
                    using (var cmd = new MySqlCommand(query, connect))
                    {
                        // Tạo tham số
                        cmd.Parameters.AddWithValue("@timeStart", timeStart);
                        cmd.Parameters.AddWithValue("@timeEnd", timeEnd);

                        // Thực hiện truy vấn
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Đọc dữ liệu truy vấn trả về
                            while (reader.Read())
                            {
                                // Tạo đối tượng Student
                                var student = new Student
                                {
                                    StudentId = reader.GetInt32("StudentId"),
                                    Name = reader.GetString("Name"),
                                    Gender = reader.GetString("Gender"),
                                    DayOfBirth = reader.GetDateTime("DayOfBirth"),
                                    Class = reader.GetString("Class"),
                                    Address = reader.GetString("Address"),
                                    Phone = reader.GetInt32("Phone"), // Use GetString
                                    JoinDay = reader.GetDateTime("JoinDay"),
                                    Parent = new Parent // Tạo đối tượng Parent
                                    {
                                        ParentName = reader.GetString("ParentName"),
                                        ParentEmail = reader.GetString("ParentEmail"),
                                        ParentPhone = reader.GetInt32("ParentPhone"), // Use GetString
                                        ParentAddress = reader.GetString("ParentAddress")
                                    }
                                };

                                // Thêm đối tượng Student vào danh sách
                                students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Lỗi tìm kiếm học sinh theo thời gian[/]");
               
            }

            return students;
        }


    }
}