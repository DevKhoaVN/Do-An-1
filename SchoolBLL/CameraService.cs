using AForge.Video;
using AForge.Video.DirectShow;
using ZXing.Windows.Compatibility;
using System.Drawing;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TestMySql.Models;
using EntryLogManagement.SchoolDAL;

namespace EntryLogManagement.SchoolBLL
{
    internal class CameraService
    {
        private readonly StudentRepository studentRepository;
        private readonly EntryLogRepository entryLogRepository;
        private readonly SoundService soundService;

        public CameraService()
        {
            studentRepository = new StudentRepository();
            entryLogRepository = new EntryLogRepository();
            soundService = new SoundService();
        }

        // Bật thiết bị camera
        public bool TurnOn(int qr)
        {
            // Kiểm tra sinh viên có ID tương ứng có tồn tại không
            List<Student> students = studentRepository.GetStudentId(qr);

            if (students.FirstOrDefault() != null)
            {
                var status = "Out"; // Trạng thái mặc định nếu không tìm thấy bản ghi trước đó

                // Lấy bản ghi ra vào mới nhất của sinh viên
                List<Entrylog> entries = entryLogRepository.GetEntryLogId(qr);

                // Xác định trạng thái log dựa trên bản ghi trước đó
                if (entries != null && entries.FirstOrDefault() != null && entries.FirstOrDefault().Status == "Int")
                {
                    status = "Out";
                }
                else
                {
                    status = "Int";
                }

                var time = DateTime.Now;
                // Tạo bản ghi ra vào mới
                Entrylog log = new Entrylog
                {
                    LogTime = time,
                    StudentId = qr,
                    Status = status
                };

                // Chèn bản ghi vào cơ sở dữ liệu và kiểm tra xem việc chèn có thành công không
                bool isInserted = entryLogRepository.InsertEntryLog(log);

                // Trả về kết quả của việc chèn bản ghi
                return isInserted;
            }

            // Nếu sinh viên không tồn tại, trả về false
            return false;
        }

    }
}






                