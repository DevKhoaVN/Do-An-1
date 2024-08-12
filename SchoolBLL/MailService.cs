using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace EntryLogManagement.SchoolBLL
{
    internal class MailService
    {
        private string _fromMail = "khoavanle2@gmail.com";
        private string _password = "jqbt ywhe tmjl xsgm"; // Ensure this is your app password or actual password
        private string Toemail = "khoavanle3@gmail.com";

        public void SendEmail(string StudentName)
        {
            using (var _mail = new MailMessage())
            {
                try
                {
                    _mail.From = new MailAddress(_fromMail);
                    _mail.To.Add(new MailAddress(Toemail));
                    _mail.Subject = $"Thông tin về học sinh {StudentName}";
                    _mail.Body = "Con của bạn hiện không thuộc quản lý của nhà trường. Vui lòng liên hệ hotline:19001006 để biết thêm thông tin chi tiết";
                    _mail.IsBodyHtml = true;

                    using (var _smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        _smtp.UseDefaultCredentials = false;
                        _smtp.Credentials = new NetworkCredential(_fromMail, _password);
                        _smtp.EnableSsl = true;

                        _smtp.Send(_mail);
                       
                    }
                }
                catch (Exception ex)
                {
                   AnsiConsole.Markup("{[red]Gửi email thất bại tới.[red] ");
                    // Handle or log the exception appropriately for your application
                }
            }
        }
    }
}
