using EntryLogManagement.SchoolBLL;
using EntryLogManagement.SchoolPL.Utility;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolPL
{
    internal class AbsentReportPL
    {
        private readonly AbsentreportService absentreportService;

        public AbsentReportPL()
        {
            absentreportService = new AbsentreportService();
        }

        public void ShowAbsenReportID()
        {

            while (true)
            {
                int id = InputHepler.GetIntPrompt("Nhập [green]id tìm kiếm : [/]");

               var absent = absentreportService.GetReportID(id);

                if (absent.Count > 0)
                {
                    // Nếu có bản ghi, hiển thị bảng báo cáo và thoát vòng lặp
                    ShowAbsentReport_Table(absent);
                    break;
                }
                else
                {
                    // Nếu không có bản ghi, thông báo và yêu cầu nhập lại
                    AnsiConsole.MarkupLine("[red]Không có bản ghi trong hệ thống. Vui lòng thử lại.[/]");
                    Console.WriteLine();
                }
            }
        }


        public void ShowAbsenReportAll()
        {

            var absent = absentreportService.GetReportAll();

            ShowAbsentReport_Table(absent);
        }

        public void ShowAbsenReportRangeTime()
        {
            while (true)
            {
                DateTime timeStart = InputHepler.GetDate("Nhập [green]ngày bắt đầu (dd/mm/yyyy): [/]");
                DateTime timeEnd = InputHepler.GetDate("Nhập [green]ngày kết thúc (dd/mm/yyyy): [/]");

                var absent = absentreportService.GetReportRangeTime(timeStart, timeEnd);

                if (absent.Count > 0)
                {
                    // Nếu có bản ghi, hiển thị bảng báo cáo và thoát vòng lặp
                    ShowAbsentReport_Table(absent);
                    break;
                }
                else
                {
                    // Nếu không có bản ghi, thông báo và yêu cầu nhập lại
                    AnsiConsole.MarkupLine("[red]Không có bản ghi học sinh nào trong hệ thống cho khoảng thời gian này. Vui lòng thử lại với khoảng thời gian khác.[/]");
                    Console.WriteLine();
                }
            }
        }

        public void ShowAbsentReport_Table(List<Absentreport> data)
        {
            int pageSize = 15;
            int totalRecords = data.Count;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            int currentPage = 1;

            while (true)
            {
                Console.Clear(); // Xóa màn hình trước khi hiển thị trang mới
                Console.WriteLine($"Trang {currentPage} / {totalPages}");

                // Tạo bảng và thêm các cột
                var table = new Table().Expand().Centered();
                table.Title($"[#ffff00]Bảng báo cáo vắng học[/]");
                table.AddColumn("ID học sinh");
                table.AddColumn("Tên học sinh");
                table.AddColumn("Tên phụ huynh");
                table.AddColumn("Lớp");
                table.AddColumn("Ngày báo cáo");
                table.AddColumn("Lý do");

                // Tính toán các dòng cần hiển thị trên trang hiện tại
                var pageData = data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                // Thêm các hàng vào bảng
                foreach (var report in pageData)
                {
                    table.AddRow(
                        $"{report.Parent.Students.StudentId}",
                        $"{report.Parent.Students.Name}",
                        $"{report.Parent.ParentName}",
                        $"{report.Parent.Students.Class}",
                        $"{report.CreateDay:yyyy-MM-dd}",
                        $"{report.Reason}"
                    );
                }

                // Hiển thị bảng
                AnsiConsole.Render(table);
                AnsiConsole.WriteLine();

                // Điều hướng người dùng
                if (totalPages > 1)
                {
                    if (currentPage < totalPages && currentPage > 1)
                    {
                        Console.WriteLine("Nhấn [Enter] để xem trang tiếp theo, [P] để quay lại trang trước, hoặc [Esc] để thoát.");
                        Console.WriteLine();
                    }
                    else if (currentPage < totalPages)
                    {
                        Console.WriteLine("Nhấn [Enter] để xem trang tiếp theo hoặc [Esc] để thoát.");
                        Console.WriteLine();
                    }
                    else if (currentPage == 1)
                    {
                        Console.WriteLine("Nhấn [Enter] để xem trang tiếp theo hoặc [Esc] để thoát.");
                        Console.WriteLine();
                    }
                    else
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
