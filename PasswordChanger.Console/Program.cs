using System;
using System.Collections.Generic;
using System.Data;
using PasswordChanger.Application.Services;
using PasswordChanger.Domain;

namespace PasswordChanger.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Password Changer service started... Press any key to continue.");
            Console.ReadKey();

            ExportDataTableTest();
            ExportOpenOrderUsersTest();
        }

        /// <summary>
        /// For testing purposes only. Write data table to CSV (local).
        /// </summary>
        public static void ExportDataTableTest()
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
                exportService.WriteDataTableToCsv(tbl, @"C:\Users\Adam.Morton\Desktop\datatable.csv", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// For testing purposes only. Write Open Order Users list to CSV (local).
        /// </summary>
        public static void ExportOpenOrderUsersTest()
        {
            var openOrderUsers = new HashSet<OpenOrderUser>()
            {
                new OpenOrderUser(1, "steve", "fef434y5hs$%^^@!", new Company(12, "Title's Title")),
                new OpenOrderUser(2, "xxxexx", "byj6767re4#Y^W$%^^@!", new Company(13, "Adam Title")),
                new OpenOrderUser(3, "ddddsss", "y4%Y^E%HGThym%^^@!", new Company(14, "Maarcl")),
                new OpenOrderUser(4, "rttggg", "'';h';45';4w'h;45'", new Company(15, "Douglas Fir")),
                new OpenOrderUser(5, "nnntrntrt", "'g4';t'3q4T%\"Y$Wh", new Company(16, "PineTree"))
            };

            var exportService = new ExportService();

            try
            {
                exportService.WriteOpenOrderUsersToCsv(openOrderUsers, @"C:\Users\Adam.Morton\Desktop\openorderusers.csv", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Console.ReadKey();
            }
        }
    }
}
