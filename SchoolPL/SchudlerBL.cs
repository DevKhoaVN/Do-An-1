using EntryLogManagement.SchoolBLL;
using EntryLogManagement.SchoolPL.Utility;
using Quartz.Impl;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace EntryLogManagement.SchoolPL
{
    internal class SchudlerBL
    {
        private readonly SchedulerService _schedulerService;

        public SchudlerBL()
        {
            _schedulerService = new SchedulerService(); ;
        
        }

        // Adjust the scheduler's time
        public async Task AdjustTimeSchedulerAsync()
        {
            // Nhận thông tin giờ và phút từ người dùng
            int hour1 = InputHepler.GetValidHour("Nhập [green]giờ bạn muốn thay đổi (buổi sáng)[/]: ");
            int minutes1 = InputHepler.GetValidMinute("Nhập [green]phút bạn muốn thay đổi (buổi sáng)[/]: ");

            int hour2 = InputHepler.GetValidHour("Nhập [green]giờ bạn muốn thay đổi (buổi chiều)[/]: ");
            int minutes2 = InputHepler.GetValidMinute("Nhập [green]phút bạn muốn thay đổi (buổi chiều)[/]: ");

            await Console.Out.WriteLineAsync();
            AnsiConsole.MarkupLine("[Green]Điều chỉnh thời gian thành công.[/]");

            await Console.Out.WriteLineAsync();
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                // Kiểm tra nếu scheduler là null
                if (scheduler == null)
                {
                    AnsiConsole.Markup("[red]Không thể lấy đối tượng scheduler.Hãy liên hệ bộ phận kĩ thuật.[/]");
                    return;
                }

                await scheduler.Shutdown(true);
                await _schedulerService.StartScheduler(hour1, minutes1, hour2, minutes2);

            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi khi điều chỉnh thời gian bộ lập lịch:[/]");
                AnsiConsole.WriteLine();
            }
        }

    }
}

