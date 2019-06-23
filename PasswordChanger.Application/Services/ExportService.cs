using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using PasswordChanger.Domain;

namespace PasswordChanger.Application.Services
{
    public class ExportService
    {
        /// <summary>
        /// Creates and exports a CSV file from a DataTable.
        /// </summary>
        public void WriteDataTableToCsv(DataTable dataTable, string saveFilePath, bool includeHeaders)
        {
            if (!saveFilePath.EndsWith(".csv"))
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(saveFilePath))
            {
                if (includeHeaders)
                {
                    IEnumerable<string> headerValues = dataTable.Columns
                        .OfType<DataColumn>()
                        .Select(column => QuoteValue(column.ColumnName));

                    writer.WriteLine(string.Join(",", headerValues));
                }

                IEnumerable<string> items = null;

                foreach (DataRow row in dataTable.Rows)
                {
                    items = row.ItemArray.Select(o => QuoteValue(o?.ToString() ?? string.Empty));
                    writer.WriteLine(string.Join(",", items));
                }

                writer.Flush();
            }
        }

        /// <summary>
        /// Creates and exports a CSV file from a list of OpenOrderUsers.
        /// </summary>
        public void WriteOpenOrderUsersToCsv(IEnumerable<OpenOrderUser> openOrderUsers, string saveFilePath, bool includeHeaders)
        {
            if (!saveFilePath.EndsWith(".csv"))
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(saveFilePath))
            {
                if (includeHeaders)
                {
                    IEnumerable<string> headerValues = new HashSet<string>()
                    {
                        "OpenOrder User ID",
                        "OpenOrder Username",
                        "OpenOrder Old Password",
                        "OpenOrder New Password",
                        "Company ID",
                        "Company Name"
                    };

                    writer.WriteLine(string.Join(",", headerValues));
                }

                List<string> items = new List<string>();

                foreach (var openOrderUser in openOrderUsers)
                {
                    items?.Clear();
                    items.Add(openOrderUser.Id.ToString());
                    items.Add(QuoteValue(openOrderUser.Username));
                    items.Add(QuoteValue(openOrderUser.OldPassword));
                    items.Add(QuoteValue(openOrderUser.NewPassword));
                    items.Add(openOrderUser.Company.Id.ToString());
                    items.Add(QuoteValue(openOrderUser.Company.Name));

                    writer.WriteLine(string.Join(",", items));
                }

                writer.Flush();
            }
        }

        /// <summary>
        /// Adding quotes to handle special characters.
        /// </summary>
        private static string QuoteValue(string value)
        {
            return string.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }

        /// <summary>
        /// For testing purposes only. Write data table to CSV (local).
        /// </summary>
        public static void ExportDataTableTest(string saveFilePath)
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
                exportService.WriteDataTableToCsv(tbl, saveFilePath, true);
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
        public static void ExportOpenOrderUsersTest(string saveFilePath)
        {
            var openOrderUsers = new HashSet<OpenOrderUser>()
            {
                new OpenOrderUser(1, "steve", "dumb1", "fef434y5hs$%^^@!", new Company(12, "Title's Title")),
                new OpenOrderUser(2, "xxxexx", "dumb2", "byj6767re4#Y^W$%^^@!", new Company(13, "Adam Title")),
                new OpenOrderUser(3, "ddddsss", "dumb3", "y4%Y^E%HGThym%^^@!", new Company(14, "Maarcl")),
                new OpenOrderUser(4, "rttggg", "dumb4", "'';h';45';4w'h;45'", new Company(15, "Douglas Fir")),
                new OpenOrderUser(5, "nnntrntrt", "dumb5", "'g4';t'3q4T%\"Y$Wh", new Company(16, "PineTree"))
            };

            var exportService = new ExportService();

            try
            {
                exportService.WriteOpenOrderUsersToCsv(openOrderUsers, saveFilePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Console.ReadKey();
            }
        }
    }
}
