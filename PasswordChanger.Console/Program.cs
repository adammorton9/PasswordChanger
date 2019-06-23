using System;
using System.Data;
using PasswordChanger.Application.Services;

namespace PasswordChanger.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Password Changer service started...Press any key to continue.");
            Console.ReadKey();

            ExportTest();
        }

        /// <summary>
        /// For testing purposes only.
        /// </summary>
        public static void ExportTest()
        {
            DataTable tbl = new DataTable();

            tbl.Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(Guid)),
                new DataColumn("Date", typeof(DateTime)),
                new DataColumn("StringValue", typeof(string)),
                new DataColumn("NumberValue", typeof(int)),
                new DataColumn("BooleanValue", typeof(bool))
            });

            tbl.Rows.Add(Guid.NewGuid(), DateTime.Now, "String1", 100, true);
            tbl.Rows.Add(Guid.NewGuid(), DateTime.Now, "String2", 200, false);
            tbl.Rows.Add(Guid.NewGuid(), DateTime.Now, "String3", 300, true);

            var exportService = new ExportService();

            try
            {
                exportService.WriteDataTableToCsv(tbl, @"C:\Users\Adam.Morton\Desktop\test.csv", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Console.ReadKey();
            }
        }
    }
}
