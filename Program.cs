using EntryLogManagement.SchoolBLL;
using EntryLogManagement.SchoolPL;
using EntryLogManagement.SchoolPL.Utility;
using Spectre.Console;
using System.Text;

internal class Program
{

    public static void Main(string[] args)
    {
        

        // Hiển thị tiếng việt
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        AnsiConsole.Write(new FigletText("EntryLogManagement").Color(Color.Aquamarine1_1).Centered());

        StudentPL studentPL = new StudentPL();
        EntryLogPL entryLogPL = new EntryLogPL();
        AlertPL alertPL = new AlertPL();
        AbsentReportPL absentReportPL = new AbsentReportPL();
        UserPL userPL = new UserPL();

        bool isCheckAll = true;
        while (isCheckAll)
        {

            switch (MainMenu.IndexMenu())
            {
                // xử lí đăng kí tài khoản
                case 1:
                    // khởi tạo và gọi phương thức đăng kí
                    userPL.Register();
                    break;

                // Xử lí đăng nhập
                case 2:

                    // xác định vai trò của user
                    var user = userPL.Login();
                    int role ;
                    int parentid;
                    if ( user == null)
                    {
                        role = -1;
                        parentid = -1;
                    }
                    else
                    {
                         role = user.RoleId;
                         parentid = user.ParentId;
                    }               
                        switch (role)
                        {
                            //Thao tác của admin
                            case 1:
                            int hour1 = 0, minute1 = 4 , hour2 = 18, minute2 = 1;
                            SchedulerService s = new SchedulerService();
                             s.StartScheduler(hour1, minute1, hour2, minute2);

                            bool isCheckAdmin = true;

                                while (isCheckAdmin)
                                {
                                
                                // Menucủa admin
                                switch (MenuAdmin.AdminMenu())
                                    {
                                        // Thực hiện 1.Quản lí học sinh
                                        case 1:
                                            bool isCheckAdmin_1 = true;
                                            while (isCheckAdmin_1)
                                            {
                                                switch (MenuAdmin.AdminStudentManagement1())
                                                {
                                                    //  1.Quản lí học sinh -> 1.Thêm xửa xóa
                                                    case 1:

                                                        bool isCheckAdmin_1_1 = true;
                                                        while (isCheckAdmin_1_1)
                                                        {
                                                            switch (MenuAdmin.AdminStudentManagement1_1())
                                                            {
                                                                // Thêm
                                                                case 1:
                                                                studentPL.AddStudent();
                                                                    break;

                                                                // Xóa
                                                                case 2:
                                                                studentPL.DeleteStudent();
                                                                    break;

                                                                // chỉnh
                                                                case 3:
                                                                studentPL.UpdateStudent();
                                                                    break;

                                                                case 0:

                                                                    // Quay lại trang trước đó
                                                                    isCheckAdmin_1_1 = false;
                                                                    break;

                                                                default:
                                                                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                                    break;
                                                            }
                                                        }
                                                        break;

                                                    // 1.Quản lí học sinh-> 2.xem báo cáo vắng học
                                                    case 2:

                                                        bool isCheckAdmin_1_2 = true;
                                                        while (isCheckAdmin_1_2)
                                                        {
                                                            switch (MenuAdmin.AdminStudentManagement1_2())
                                                            {
                                                                // Học theo id học sinh
                                                                case 1:

                                                                    absentReportPL.ShowAbsenReportID();
                                                                    break;

                                                                // Lọc theo thời gian
                                                                case 2:

                                                                    absentReportPL.ShowAbsenReportRangeTime();
                                                                    break;

                                                                // Hiển thị tất cả
                                                                case 3:

                                                                   absentReportPL.ShowAbsenReportAll();
                                                                    break;

                                                                // Quay lại trang trước đó
                                                                case 0:

                                                                    isCheckAdmin_1_2 = false;
                                                                    break;

                                                                default:
                                                                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                                    break;
                                                            }
                                                        }
                                                        break;

                                                    // 1.Quản lí học sinh -> 3.Xem cảnh báo
                                                    case 3:

                                                        bool isCheckAdmin_1_3 = true;
                                                        while (isCheckAdmin_1_3)
                                                        {
                                                            switch (MenuAdmin.AdminStudentManagement1_3())
                                                            {
                                                                // Hiển thị cnahr báo ngày hôm nay
                                                                case 1:

                                                                    alertPL.ShowAlertToday();
                                                                    break;

                                                                // Lọc theo thời gian
                                                                case 2:

                                                                    alertPL.ShowAlertAll();
                                                                    break;
                                                                
                                                                // Quay lại trang trước đó
                                                                case 0:

                                                                    isCheckAdmin_1_3 = false;
                                                                    break;

                                                                default:
                                                                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                                    break;
                                                            }
                                                        }
                                                        break;

                                                    //1.Quản lí học sinh -> 4.Xem thông tin học sinh
                                                    case 4:

                                                        bool isCheckAdmin_1_4 = true;
                                                        while (isCheckAdmin_1_4)
                                                        {
                                                            switch (MenuAdmin.AdminStudentManagement1_4())
                                                            {
                                                                // Lọc theo id học sinh
                                                                case 1:

                                                                    studentPL.ShowStudentInforÌD();
                                                                    break;

                                                                // Lọc theo lớp
                                                                case 2:

                                                                studentPL.ShowStudentInforByRangeTime();
                                                                    break;

                                                                // Hiển thị tất cả
                                                                case 3:
                                                                    studentPL.ShowStudentInforAll();
                                                                    break;

                                                                // Quay lại trang trước đó
                                                                case 0:

                                                                    isCheckAdmin_1_4 = false;
                                                                    break;

                                                                default:
                                                                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                                    break;
                                                            }
                                                        }
                                                        break;

                                                    case 0:

                                                        isCheckAdmin_1 = false;
                                                        break;

                                                    default:
                                                        Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                        break;
                                                }
                                            }

                                            break;

                                        // Thực hiện 2.Quản lí ra vào --------------------------------
                                        case 2:

                                            bool isCheckEntry = true;
                                            while (isCheckEntry)
                                            {
                                                switch (MenuAdmin.AdminEntryLogManagement2())
                                                {
                                                    // 2.Quản lí ra vào -> 1.Xem bảng học sinh ra vào
                                                    case 1:

                                                        bool isCheckEntry_1 = true;
                                                        while (isCheckEntry_1)
                                                        {
                                                            switch (MenuAdmin.AdminEntryLogManagement2_1())
                                                            {
                                                                // Học theo id học sinh
                                                                case 1:

                                                                   entryLogPL.ShowEntryLogID();
                                                                    break;

                                                                // Lọc theo thời gian
                                                                case 2:

                                                                    entryLogPL.ShowEntryLogRangeTime();
                                                                    break;

                                                                // Hiển thị tất cả
                                                                case 3:
 
                                                                    entryLogPL.ShowEntryLogAll();
                                                                    break;

                                                                // Quay lại trang trước đó
                                                                case 0:

                                                                    isCheckEntry_1 = false;
                                                                    break;

                                                                default:
                                                                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                                    break;
                                                            }
                                                        }

                                                        break;

                                                    //2.Quản lí ra vào -> 3.Điều chỉnh thời gian cảnh báo 
                                                    case 2:

                                                    SchudlerBL schudlerBL = new SchudlerBL();
                                                     schudlerBL.AdjustTimeSchedulerAsync( );
                                                    break;

                                                // Quay lại trang trước đó
                                                case 0:
                                                        isCheckEntry = false;
                                                        break;


                                                    default:
                                                        Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                        break;

                                                }

                                            }
                                            break;

                                        // Thực hiện 3.Thực hiện kiểm tra ra vào
                                        case 3:

                                        entryLogPL.RecoredEntryLog();
                                        break;

                                        case 0:

                                            return;
                                    }

                                }
                                break;

                            // Thao tác của parent
                            case 2:

                            ParentPL parentPL = new ParentPL();
                                bool isCheckParent = true;
                                while (isCheckParent)
                                {
                                    switch (MenuParent.ParentMenu())
                                    {
                                        // 1. Xem thông tin ra vào
                                        case 1:

                                            bool isCheckParent1 = true;
                                            while (isCheckParent1)
                                            {
                                                switch (MenuParent.ParentEntryLog())
                                                {
                                                    // Lọc theo thời gian
                                                    case 1:

                                                        parentPL.ShowLogStudentRangeTime(parentid);
                                                        break;

                                                    // Hiển thị tất cả
                                                    case 2:

                                                       parentPL.ShowLogStudentAll(parentid);
                                                        break;

                                                    case 0:

                                                        isCheckParent1 = false;
                                                        break;

                                                    default:
                                                        Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                        break;
                                                }
                                            }
                                            break;

                                        // 2. Xem báo cáo vắng học
                                        case 2:

                                            bool isCheckParent2 = true;
                                            while (isCheckParent2)
                                            {
                                                switch (MenuParent.ParentAbsentReport())
                                                {
                                                    // Gửi báo cáo vắng học
                                                    case 1:

                                                        parentPL.SendReport(parentid);
                                                        break;

                                                    // Hiển thị tất cả
                                                    case 2:
                                                       parentPL.ShowAbsentStudentAll(parentid);
                                                        break;

                                                    case 0:

                                                        isCheckParent2 = false;
                                                        break;

                                                    default:
                                                        Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                                        break;
                                                }
                                            }
                                            break;

                                        case 0:

                                            return;

                                        default:
                                            Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                                            break;
                                    }
                                }

                                break;
                        }

                    break;

                case 0:

                    isCheckAll = false;
                    break;

                default:

                    Console.WriteLine("Vui lòng nhập đúng lựa chọn!");
                    break;
            }
        }

    }
}