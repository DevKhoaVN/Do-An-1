using EntryLogManagement.SchoolBLL;
using EntryLogManagement.SchoolDAL;
using EntryLogManagement.SchoolPL.Utility;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolPL
{
    internal class UserPL
    {
        private readonly UserService userService;
        private readonly HandleLogRepository logRepository;

        public UserPL()
        {
            userService = new UserService();
            logRepository = new HandleLogRepository();
          

        }

        public User Login()
        {
            while (true)
            {
                string UserName = InputHepler.PromptUserInput("Nhập [green]UserName: [/]");
                string Password = InputHepler.PromptUserInput("Nhập [green]Password: [/]");
                Console.WriteLine();

                var reuslt =  userService.LoginUser(UserName, Password);

                if (reuslt != null)
                {
                    AnsiConsole.MarkupLine("[green]Bạn đã đăng nhập thành công[/]");
                    AnsiConsole.WriteLine();
                    return reuslt;
                    break;
                }

                AnsiConsole.MarkupLine("[red]Bạn đã nhập sai tài khoản hoặc mật khẩu[/]");
                Console.WriteLine();

            }
                
            
        }

        public void Register()
        {
            while (true)
            {
                // Nhập tên người dùng
                string userName = InputHepler.PromptUserInput("Nhập [green]UserName: [/]");

                // Kiểm tra xem tên người dùng đã tồn tại chưa
                if (!logRepository.HandleUserName(userName))
                {
                    AnsiConsole.MarkupLine("[red]Tên người dùng đã tồn tại. Vui lòng chọn tên khác.[/]");
                    Console.WriteLine();
                    continue; // Yêu cầu nhập lại tên người dùng
                }

                // Nhập mật khẩu
                string password = InputHepler.PromptUserInput("Nhập [green]Password: [/]");

            re_enter:
                // Nhập ID người dùng
                int id = InputHepler.GetIntPrompt("Nhập [green]ID của bạn : [/]");


                // Đăng ký người dùng
                var result = userService.RegisterUser(userName, password, id);
                if (result)
                {
                    AnsiConsole.Markup("[green]Bạn đã đăng kí thành công[/]");
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    goto re_enter;

                }

            }
        }



    }

}
