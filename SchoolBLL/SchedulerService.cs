using System;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TestMySql.Models;
using EntryLogManagement.SchoolDAL;
using Spectre.Console;

namespace EntryLogManagement.SchoolBLL
{
    internal class SchedulerService
    {
        // Phương thức khởi động bộ lập lịch (scheduler) với các tham số thời gian mặc định và tùy chọn
        public async Task StartScheduler(int hour1 , int minute1 , int hour2 , int minute2 )
        {
            try
            {
                // Tạo StdSchedulerFactory để quản lí lịch
                StdSchedulerFactory factory = new StdSchedulerFactory();
                var scheduler = await factory.GetScheduler(); // Lấy đối tượng scheduler
                await scheduler.Start(); // Bắt đầu chạy scheduler

                // job 1 
                IJobDetail job1 = JobBuilder.Create<PhatCanhBaoJob>()
                    .WithIdentity("PhatCanhBaoJobNhom1", "Nhom1") // Đặt tên công việc và nhóm
                    .Build();

                // lịch trình công việc 1
                ITrigger trigger1 = TriggerBuilder.Create()
                    .WithIdentity("TriggerPhatCanhBaoNhom1", "Nhom1") // Đặt tên lịch trình và nhóm
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour1, minute1)) // Lên lịch hàng ngày tại thời gian cụ thể
                    .Build();

                // Job 2
                IJobDetail job2 = JobBuilder.Create<PhatCanhBaoJob>()
                    .WithIdentity("PhatCanhBaoJobNhom2", "Nhom2") // Đặt tên công việc và nhóm
                    .Build();

                // Tlcihj trình công việc 2
                ITrigger trigger2 = TriggerBuilder.Create()
                    .WithIdentity("TriggerPhatCanhBaoNhom2", "Nhom2") // Đặt tên lịch trình và nhóm
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour2, minute2)) // Lên lịch hàng ngày tại thời gian cụ thể
                    .Build();

                // lên lịch công việc với ác lịch tương ứng
                await scheduler.ScheduleJob(job1, trigger1);
                await scheduler.ScheduleJob(job2, trigger2);
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Đã xảy ra lỗi khi khởi động bộ lập lịch:[/]");
                AnsiConsole.WriteLine();
            }
        }

        // Định nghĩa công việc phát cảnh báo
        public class PhatCanhBaoJob : IJob
        {
            // Phương thức thực thi công việc
            public async Task Execute(IJobExecutionContext context)
            {
                try
                {
                    // Khởi tạo các dịch vụ gửi email và âm thanh và nạp các lớp DAL
                    var entryLogRepository = new EntryLogRepository();
                    var alertRepository = new AlertRepository();
                    var mailService = new MailService();
                    var soundService = new SoundService();

                    var students = entryLogRepository.GetEntryLogToAlert();
                    if (students.Any())
                    {
                        foreach (var item in students)
                        {
                            mailService.SendEmail(item.Student.Name);
                        }

                        foreach (var item in students)
                        {
                            alertRepository.InsertAlert(item.StudentId, DateTime.Now);
                        }

                        // Phát âm thanh thông báo
                        soundService.PlaySoundLog();
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]Không có đối tượng trả về.[/]");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.Markup($"[red]Đã xảy ra lỗi khi thực thi công việc phát cảnh báo:[/] {ex.Message}");
                    AnsiConsole.WriteLine();
                }

                await Task.CompletedTask;
            }
        }

     
        }
    }

