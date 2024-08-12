using EntryLogManagement.SchoolBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using TestMySql.Models;
using EntryLogManagement.SchoolPL.Utility;
using System.Runtime.CompilerServices;

namespace EntryLogManagement.SchoolPL
{
    internal class StudentPL
    {

        private readonly StudentService studentService;
        

        public StudentPL()
        {
            studentService = new StudentService();
        }
        public void ShowStudentInforID()
        {
            while (true)
            {
                int id = InputHepler.GetIntPrompt("Nhập [green]id: [/]");

                var students = studentService.GetStudentID(id);

                if (students.Count > 0)
                {
                    // Nếu tìm thấy học sinh, hiển thị thông tin và thoát vòng lặp
                    StudentInfor_Table(students);
                    break;
                }
                else
                {
                    // Nếu không tìm thấy học sinh, thông báo và yêu cầu nhập lại ID
                    AnsiConsole.MarkupLine("[red]Học sinh không tồn tại,[/] vui lòng nhập lại.");
                    Console.WriteLine();
                }
            }
        }


        public void ShowStudentInforAll()
        {
            var student = studentService.GetStudentAll();

            StudentInfor_Table(student);
        }

        public void ShowStudentInforByRangeTime()
        {
            while (true)
            {
                DateTime timeStart = InputHepler.GetDate("Nhập [green]ngày bắt đầu (dd/mm/yyyy): [/]");
                DateTime timeEnd = InputHepler.GetDate("Nhập [green]ngày kết thúc (dd/mm/yyyy): [/]");

                var students = studentService.GetStudentByRangeTime(timeStart, timeEnd);

                if (students.Count > 0)
                {
                    // Nếu có bản ghi sinh viên, hiển thị thông tin và thoát vòng lặp
                    StudentInfor_Table(students);
                    break;
                }
                else
                {
                    Console.WriteLine();
                    // Nếu không có bản ghi, thông báo và yêu cầu nhập lại
                    AnsiConsole.MarkupLine("[red]Không có bản ghi học sinh nào trong hệ thống cho khoảng thời gian này. Vui lòng thử lại với khoảng thời gian khác.[/]");
                    Console.WriteLine();
                }
            }
        }


        // Thêm học sinh
        public void AddStudent()
        {
            var student = InputHepler.GetStudentAndParentInfo();

            var result = studentService.AddStudent(student);

            if(result)
            {
                Console.WriteLine();
                AnsiConsole.MarkupLine("[green]Bạn đã thêm học sinh thành công.[/]");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                AnsiConsole.MarkupLine("[red]Bạn đã thêm học sinh thất bại.[/]");
                Console.WriteLine();
            }
        }

        public void DeleteStudent()
        {
            re_enter:

            int id = InputHepler.GetIntPrompt("Nhập[green] id học sinh muốn xóa : [/]");

            var result = studentService.DeleteStudent(id);
            while (true)
            {

                if (result)
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[green]Bạn đã xóa học sinh thành công.[/]");
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[red]Học sinh không tồn tại trong hệ thống.[/]");
                    goto re_enter;
                }
            }
           

        }

        public void UpdateStudent()
        {
            while (true) 
            {
                Console.WriteLine();
                var id = InputHepler.GetIntPrompt("Nhập[green] id học sinh muốn chỉnh sửa : [/]");

                var studentExists = studentService.GetStudentID(id).FirstOrDefault();

                if (studentExists == null)
                {
                    AnsiConsole.MarkupLine("[red]Học sinh không tồn tại. Vui lòng nhập lại.[/]");
                    Console.WriteLine();
                    continue; 
                }

                var student = InputHepler.EnterStudent(studentExists);

                var result = studentService.UpdateStudent(student, id);

                if (result)
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[green]Bạn đã cập nhật học sinh thành công.[/]");
                    Console.WriteLine();
                    break; 
                }
                else
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[red]Có lỗi xảy ra khi cập nhật học sinh. Vui lòng thử lại.[/]");
                    Console.WriteLine();
                }
            }
        }

        public void StudentInfor_Table(List<Student> studentInfor)
        {
            int pageSize = 15;
            int totalRecords = studentInfor.Count;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            int currentPage = 1;

            while (true)
            {
                Console.Clear(); // Xóa màn hình trước khi hiển thị trang mới
                Console.WriteLine($"Trang {currentPage} / {totalPages}");

                // Tạo bảng và thêm các cột
                var table = new Table().Centered();
                table.Title("[yellow]Danh sách thông tin học sinh[/]").HeavyEdgeBorder();
                table.AddColumn("ID học sinh");
                table.AddColumn("Tên học sinh");
                table.AddColumn("Giới tính");
                table.AddColumn("Ngày sinh");
                table.AddColumn("Lớp");
                table.AddColumn("Địa chỉ");
                table.AddColumn("Số điện thoại");
                table.AddColumn("Tên phụ huynh");
                table.AddColumn("Email phụ huynh");
                table.AddColumn("Số điện thoại phụ huynh");
                table.AddColumn("Địa chỉ phụ huynh");

                // Tính toán các dòng cần hiển thị trên trang hiện tại
                var pageData = studentInfor.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                // Thêm dữ liệu vào hàng
                foreach (var student in pageData)
                {
                    table.AddRow(
                        $"{student.StudentId}",
                        $"{student.Name}",
                        $"{student.Gender}",
                        $"{student.DayOfBirth:yyyy-MM-dd}",
                        $"{student.Class}",
                        $"{student.Address}",
                        $"{student.Phone}",
                        $"{student.Parent.ParentName}",
                        $"{student.Parent.ParentEmail}",
                        $"{student.Parent.ParentPhone}",
                        $"{student.Parent.ParentAddress}"
                    );
                }

                // Hiển thị bảng
                AnsiConsole.Render(table);
                AnsiConsole.WriteLine();

                // Điều hướng người dùng
                if (totalPages > 1)
                {
                    if (currentPage > 1 && currentPage < totalPages)
                    {
                        Console.WriteLine("Nhấn [Enter] để xem trang tiếp theo, [P] để quay lại trang trước, hoặc [Esc] để thoát.");
                        Console.WriteLine();
                    }
                    else if (currentPage == 1)
                    {
                        Console.WriteLine("Nhấn [Enter] để xem trang tiếp theo hoặc [P] để quay lại trang trước.");
                        Console.WriteLine();
                    }
                    else if (currentPage == totalPages)
                    {
                        Console.WriteLine("Nhấn [P] để quay lại trang trước hoặc [Esc] để thoát.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Nhấn [Esc] để thoát.");
                    Console.WriteLine();
                }

                // Nhận đầu vào từ người dùng để điều hướng
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    if (currentPage < totalPages)
                    {
                        currentPage++;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (key.Key == ConsoleKey.P)
                {
                    if (currentPage > 1)
                    {
                        currentPage--;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }


    }
}
