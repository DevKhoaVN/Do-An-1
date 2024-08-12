using Spectre.Console;

namespace EntryLogManagement.SchoolPL.Utility
{
    public class MenuAdmin
    {

        // Menu chính của admin
        public static int AdminMenu()
        {

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn [[[yellow]Quản lí Admin [/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Quản lí học sinh",
                        "2. Quản lí ra vào",
                        "3. Thực hiện kiểm tra ra vào",
                        "0. Thoát"
                    }));


            int choice = choose switch
            {
                "1. Quản lí học sinh" => 1,
                "2. Quản lí ra vào" => 2,
                "3. Thực hiện kiểm tra ra vào" => 3,
                "0. Quay lại trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }


        public static int AdminStudentManagement1()
        {

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí học sinh[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Thêm, sửa, xóa học sinh",
                        "2. Xem báo cáo vắng học",
                        "3. Xem cảnh báo",
                        "4. Xem thông tin học sinh",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Thêm, sửa, xóa học sinh" => 1,
                "2. Xem báo cáo vắng học" => 2,
                "3. Xem cảnh báo" => 3,
                "4. Xem thông tin học sinh" => 4,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }

        public static int AdminStudentManagement1_1()
        {

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí học sinh/Thêm, sửa, xóa học sinh[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Thêm học sinh",
                        "2. Xóa học sinh",
                        "3. Chỉnh sửa thông tin học sinh",
                        "0. Quay về trang trước đó"
                    }));

            // Mapping the selected option to an integer value
            int choice = choose switch
            {
                "1. Thêm học sinh" => 1,
                "2. Xóa học sinh" => 2,
                "3. Chỉnh sửa thông tin học sinh" => 3,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }

        public static int AdminStudentManagement1_2()
        {


            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí học sinh/Báo cáo vắng học[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Lọc theo id học sinh",
                        "2. Lọc theo thời gian",
                        "3. Hiển thị tất cả",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Lọc theo id học sinh" => 1,
                "2. Lọc theo thời gian" => 2,
                "3. Hiển thị tất cả" => 3,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }


        public static int AdminStudentManagement1_3()
        {


            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí học sinh/Xem cảnh báo[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Hiển thị bảng cảnh báo ngày hôm nay",
                        "2. Hiển thị tất cả cảnh báo",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Hiển thị bảng cảnh báo ngày hôm nay" => 1,
                "2. Hiển thị tất cả cảnh báo" => 2,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }

        public static int AdminStudentManagement1_4()
        {


            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí học sinh/Thông tin học sinh[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Lọc theo id học sinh",
                        "2. Lọc theo thời gian",
                        "3. Hiển thị tất cả",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Lọc theo id học sinh" => 1,
                "2. Lọc theo thời gian" => 2,
                "3. Hiển thị tất cả" => 3,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }

        // =================================================================================================
        // Menu Admin 2
        public static int AdminEntryLogManagement2()
        {

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí ra vào[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Xem bảng học sinh ra vào",
                        "2. Điều chỉnh thời gian cảnh báo",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Xem bảng học sinh ra vào" => 1,
                "2. Điều chỉnh thời gian cảnh báo" => 2,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }

        public static int AdminEntryLogManagement2_1()
        {

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn[[[yellow]Quản lí Admin/Quản lí ra vào/Học sinh ra vào[/]]]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Lọc theo id học sinh",
                        "2. Lọc theo thời gian",
                        "3. Hiển thị tất cả",
                        "0. Quay về trang trước đó"
                    }));

            // Mapping the selected option to an integer value
            int choice = choose switch
            {
                "1. Lọc theo id học sinh" => 1,
                "2. Lọc theo thời gian" => 2,
                "3. Hiển thị tất cả" => 3,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }


        


        // Menu admin -> bảng ra vào
        public static int AdminEntry()
        {
            AnsiConsole.Write(new Rule("[yellow]Chào mừng đến với bảng xem ra vào[/]"));

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Lọc theo id học sinh",
                        "2. Lọc theo thời gian",
                        "3. Hiển thị tất cả",
                        "0. Quay về trang trước đó"
                    }));


            int choice = choose switch
            {
                "1. Lọc theo id học sinh" => 1,
                "2. Lọc theo thời gian" => 2,
                "3. Hiển thị tất cả" => 3,
                "0. Quay về trang trước đó" => 0,
                _ => 0
            };

            return choice;
        }


    }
}
