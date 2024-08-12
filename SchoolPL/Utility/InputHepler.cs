using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestMySql.Models;

namespace EntryLogManagement.SchoolPL.Utility
{
    internal class InputHepler
    {
        public static string PromptUserInput(string promptMessage)
        {
            AnsiConsole.Markup(promptMessage);
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.MarkupLine("[red]Không được bỏ trống thông tin. Vui lòng nhập lại.[/]");
                }
                else break;
            }
            return input;
        }

        public static int GetValidHour(string prompt)
        {
            int hour;
            while (true)
            {
                AnsiConsole.Markup(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out hour) && hour >= 0 && hour <= 23)
                {
                    break;
                }
                AnsiConsole.MarkupLine("[red]Giờ không hợp lệ. Vui lòng nhập giá trị từ 0 đến 23.[/]");
            }
            return hour;
        }

        // Hàm kiểm tra mật khẩu có chứa ít nhất một ký tự đặc biệt
        public static string GetValidPassword(string prompt)
        {
            string password;

            while (true)
            {
                // Hiển thị prompt và nhận đầu vào từ người dùng
                AnsiConsole.Markup(prompt);
                password = Console.ReadLine();

                // Kiểm tra nếu mật khẩu không hợp lệ
                if (string.IsNullOrEmpty(password))
                {
                    AnsiConsole.MarkupLine("[red]Mật khẩu không được để trống. Vui lòng thử lại.[/]");
                }
                else if (!IsPasswordValid(password))
                {
                    AnsiConsole.MarkupLine("[red]Mật khẩu phải chứa ít nhất một ký tự đặc biệt. Vui lòng thử lại.[/]");
                }
                else
                {
                    // Nếu mật khẩu hợp lệ, thoát vòng lặp
                    break;
                }
            }

            return password;
        }

        private static bool IsPasswordValid(string password)
        {
            // Kiểm tra sự tồn tại của ít nhất một ký tự đặc biệt
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }



        public static int GetValidMinute(string prompt)
        {
            int minute;
            while (true)
            {
                AnsiConsole.Markup(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out minute) && minute >= 0 && minute <= 59)
                {
                    break;
                }
                AnsiConsole.MarkupLine("[red]Phút không hợp lệ. Vui lòng nhập giá trị từ 0 đến 59.[/]");
            }
            return minute;
        }

        public static string GetValidEmail(string prompt)
        {
            string email;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            while (true)
            {
                AnsiConsole.Markup(prompt);
                email = Console.ReadLine();

                if (Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
                {
                    break;
                }
                AnsiConsole.MarkupLine("[red]Địa chỉ email không hợp lệ! Vui lòng nhập một địa chỉ email hợp lệ.[/]");
            }

            return email;
        }


        public static int GetValidPhoneNumber(string prompt)
        {
            string input;
            // Regular expression pattern for a 10-digit phone number
            string phoneNumberPattern = @"^\d{10}$";

            while (true)
            {
                // Display prompt for user input
                AnsiConsole.Markup(prompt);

                // Read input from the console
                input = Console.ReadLine();

                // Validate input against the phone number pattern
                if (Regex.IsMatch(input, phoneNumberPattern))
                {
                    // Convert valid input to integer
                    if (int.TryParse(input, out int phoneNumber))
                    {
                        return phoneNumber;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Không thể chuyển đổi từ string sang int.[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Số điện thoại của bạn không đúng. Vui lòng nhập lại (10 chữ số).[/]");
                }
            }
        }



        public static int GetIntPrompt(string prompt)
        {
            int result = 0;
            bool isValid = false;

            while (!isValid)
            {
                // Hiển thị thông báo và nhận đầu vào dưới dạng chuỗi
                AnsiConsole.Markup(prompt);
                var input = Console.ReadLine();

                // Kiểm tra xem đầu vào có phải là số nguyên không
                isValid = int.TryParse(input, out result);

                // Nếu đầu vào không hợp lệ, hiển thị thông báo lỗi
                if (!isValid)
                {
                    AnsiConsole.MarkupLine("[red]Đầu vào không phải là số nguyên. Vui lòng thử lại.[/]");
                }
            }

            return result;
        }
        // vadidatw sp nguyen
        public static int GetIntPrompt()
        {
            int result = -1;  // Khởi tạo với một giá trị không hợp lệ
            bool isValid = false;  // Cờ kiểm tra hợp lệ

            do
            {
                // Thông báo mặc định cho người dùng
                string prompt = "Nhập [green]ID học sinh muốn gửi báo cáo (hoặc nhấn Enter để bỏ qua, mặc định là 0):[/]";

                // Nhận chuỗi đầu vào từ người dùng
                var input = AnsiConsole.Prompt(
                    new TextPrompt<string>(prompt)
                        .PromptStyle("green")
                        .ValidationErrorMessage("[red]Vui lòng nhập một số nguyên hoặc để trống![/]")
                        .AllowEmpty());

                // Kiểm tra nếu người dùng không nhập gì
                if (string.IsNullOrWhiteSpace(input))
                {
                    result = 0;  // Gán giá trị mặc định
                    isValid = true;  // Đánh dấu là hợp lệ
                }
                else
                {
                    // Thử chuyển đổi chuỗi thành số nguyên
                    isValid = int.TryParse(input, out result) && result >= 0;
                    if (!isValid)
                    {
                        AnsiConsole.Markup("[red]ID không hợp lệ, vui lòng nhập một số nguyên không âm![/]");
                    }
                }

            } while (!isValid);  // Tiếp tục cho đến khi người dùng nhập giá trị hợp lệ

            return result;
        }

        public static DateTime GetDate(string prompt)
        {
            string[] formats = new[]
            {
            "dd/MM/yyyy HH:mm",  // Ví dụ: 25/12/2024 14:30
            "MM/dd/yyyy HH:mm",  // Ví dụ: 12/25/2024 14:30
            "yyyy-MM-dd HH:mm",  // Ví dụ: 2024-12-25 14:30
            "dd-MM-yyyy HH:mm",  // Ví dụ: 25-12-2024 14:30
            "yyyy/MM/dd HH:mm",  // Ví dụ: 2024/12/25 14:30
            "dd/MM/yyyy",        // Ví dụ: 25/12/2024
            "MM/dd/yyyy",        // Ví dụ: 12/25/2024
            "yyyy-MM-dd",        // Ví dụ: 2024-12-25
            "dd-MM-yyyy"         // Ví dụ: 25-12-2024
             };

            while (true)
            {
                AnsiConsole.Markup(prompt); // Hiển thị lời nhắc cho người dùng
                string input = Console.ReadLine(); // Đọc dữ liệu đầu vào

                // Thử chuyển đổi đầu vào thành kiểu DateTime với các định dạng khác nhau
                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date; // Trả về giá trị DateTime nếu chuyển đổi thành công
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Định dạng ngày giờ không hợp lệ. Vui lòng nhập lại theo định dạng sau: dd-MM-yyyy.[/]");
                    AnsiConsole.WriteLine();
                }
            }
        }

        // Nhập phụ huynh và học sinh để thêm
        public static Student GetStudentAndParentInfo()
        {
            // Nhập thông tin phụ huynh
            var parent = new Parent
            {
                ParentName = PromptUserInput("Nhập[green] tên phụ huynh: [/]"),
                ParentEmail = GetValidEmail("Nhập[green] email phụ huynh: [/]"),
                ParentPhone = GetValidPhoneNumber("Nhập[green] số điện thoại phụ huynh: [/]"),
                ParentAddress = PromptUserInput("Nhập[green]  địa chỉ phụ huynh: [/]")
            };

            // Nhập thông tin học sinh
            var student = new Student
            {
                Parent = parent,
                Name = PromptUserInput("Nhập[green] tên học sinh: [/]"),
                Gender = PromptUserInput("Nhập[green] giới tính học sinh (Nam/Nữ): [/]"),
                DayOfBirth = GetDate("Nhập[green] ngày sinh học sinh (dd/MM/yyyy): [/]"),
                Class = PromptUserInput("Nhập[green] lớp học sinh: [/]"),
                Address = PromptUserInput("Nhập[green] địa chỉ học sinh: [/]"),
                Phone = GetValidPhoneNumber("Nhập[green] số điện thoại học sinh: [/]"),
                JoinDay = GetDate("Nhập[green] ngày nhập học sinh (dd/MM/yyyy): [/]")
            };

            return student;
        }


        public static Student EnterStudent(Student existingStudent)
        {
            AnsiConsole.MarkupLine("Nhập [green]thông tin học sinh (ấn Enter để giữ nguyên giá trị cũ): [/]");
            Console.WriteLine();

            // Name
            Console.Write($"Tên học sinh (hiện tại: {existingStudent.Name}): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                existingStudent.Name = name;
            }

            // Gender
            Console.Write($"Giới tính (hiện tại: {existingStudent.Gender}): ");
            var gender = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gender))
            {
                existingStudent.Gender = gender;
            }

            // Day of Birth
            Console.Write($"Ngày sinh (hiện tại: {existingStudent.DayOfBirth:yyyy-MM-dd}): ");
            var dayOfBirthInput = Console.ReadLine();
            if (DateTime.TryParse(dayOfBirthInput, out var dayOfBirth))
            {
                existingStudent.DayOfBirth = dayOfBirth;
            }

            // Class
            Console.Write($"Lớp (hiện tại: {existingStudent.Class}): ");
            var classInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(classInput))
            {
                existingStudent.Class = classInput;
            }

            // Address
            Console.Write($"Địa chỉ (hiện tại: {existingStudent.Address}): ");
            var address = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(address))
            {
                existingStudent.Address = address;
            }

            // Phone
            Console.Write($"Số điện thoại (hiện tại: {existingStudent.Phone}): ");
            var phoneInput = Console.ReadLine();
            if (int.TryParse(phoneInput, out var phone) && phone != 0)
            {
                existingStudent.Phone = phone;
            }

            // Join Day
            Console.Write($"Ngày nhập học (hiện tại: {existingStudent.JoinDay:yyyy-MM-dd}): ");
            var joinDayInput = Console.ReadLine();
            if (DateTime.TryParse(joinDayInput, out var joinDay))
            {
                existingStudent.JoinDay = joinDay;
            }

            // Parent Name
            Console.Write($"Tên phụ huynh (hiện tại: {existingStudent.Parent.ParentName}): ");
            var parentName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(parentName))
            {
                existingStudent.Parent.ParentName = parentName;
            }

            // Parent Email
            Console.Write($"Email phụ huynh (hiện tại: {existingStudent.Parent.ParentEmail}): ");
            var parentEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(parentEmail))
            {
                 existingStudent.Parent.ParentEmail = parentEmail;
            }

            // Parent Phone
            Console.Write($"Số điện thoại phụ huynh (hiện tại: {existingStudent.Parent.ParentPhone}): ");
            var parentPhoneInput = Console.ReadLine();
            if (int.TryParse(parentPhoneInput, out var parentPhone) && parentPhone != 0)
            {
                existingStudent.Parent.ParentPhone = parentPhone;
            }

            // Parent Address
            Console.Write($"Địa chỉ phụ huynh (hiện tại: {existingStudent.Parent.ParentAddress}): ");
            var parentAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(parentAddress))
            {
                existingStudent.Parent.ParentAddress = parentAddress;
            }


            return existingStudent;
        }




    }
}
