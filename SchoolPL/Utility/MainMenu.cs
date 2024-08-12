using Spectre.Console;

namespace EntryLogManagement.SchoolPL.Utility
{
    internal class MainMenu
    {
        public static int IndexMenu()
        {
            AnsiConsole.Write(new Markup("[[[bold yellow]Chào mừng đến với Quản Lí Ra Vào[/]]]"));

            AnsiConsole.WriteLine("\n");

            var choose = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Chọn một tùy chọn :")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Đăng kí",
                        "Đăng nhập"
                    }));

            // Mapping the selected option to an integer value
            int choice = choose == "Đăng kí" ? 1 : 2;
            return choice;
        }
    }
}
