using EntryLogManagement.SchoolBLL;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolPL
{
    internal class AlertPL
    {
        private readonly AlertService alertService;

        public AlertPL()
        {
            alertService = new AlertService();
        }

        public void ShowAlertToday()
        {
             var alert = alertService.GetAlertToday();

            ShowAlert_Table(alert);

        }

        public void ShowAlertAll()
        {
            var alert = alertService.GetAlertAll();

            ShowAlert_Table(alert);

        }
        public void ShowAlert_Table(List<Alert> alerts)
        {
            int pageSize = 15;
            int totalRecords = alerts.Count;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            int currentPage = 1;

            while (true)
            {
                Console.Clear(); // Xóa màn hình trước khi hiển thị trang mới
                Console.WriteLine($"Trang {currentPage} / {totalPages}");

                // Tạo bảng và thêm các cột
                var table = new Table().Expand().Centered();
                table.Title($"[#ffff00]Danh sách cảnh báo[/]").HeavyEdgeBorder();
                table.AddColumn("Tên học sinh");
                table.AddColumn("Lớp");
                table.AddColumn("Tên phụ huynh");
                table.AddColumn("Số điện thoại");
                table.AddColumn("Địa chỉ");
                table.AddColumn("Thời gian cảnh báo");

                // Tính toán các dòng cần hiển thị trên trang hiện tại
                var pageData = alerts.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                // Thêm các hàng vào bảng
                foreach (var alert in pageData)
                {
                    table.AddRow(
                        $"{alert.Student.Name}",
                        $"{alert.Student.Class}",
                        $"{alert.Student.Parent.ParentName}",
                        $"{alert.Student.Parent.ParentPhone}",
                        $"{alert.Student.Parent.ParentAddress}",
                        $"{alert.AlertTime:yyyy-MM-dd HH:mm:ss}"
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
