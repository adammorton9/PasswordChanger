using System;
using PasswordChanger.Application.Services;

namespace PasswordChanger.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Password Changer service started...");

            string filePath = GetFilePathForExport(args);

            Console.WriteLine($"Exporting .csv to: {filePath}");
            Console.WriteLine($"Press any key to start...");
            Console.ReadKey();

            //ExportService.ExportDataTableTest(args[0]);
            ExportService.ExportOpenOrderUsersTest(filePath);

            Console.WriteLine($"Export complete. Press any key to close...");
            Console.ReadKey();
        }


        /// <summary>
        /// File path can be passed in as an argument or will be prompted for if no arguments are provided.
        /// </summary>
        private static string GetFilePathForExport(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter a full file path to save the .csv export: ");
                return Console.ReadLine();
            }
            else
            {
                return args[0];
            }
        }
    }
}
