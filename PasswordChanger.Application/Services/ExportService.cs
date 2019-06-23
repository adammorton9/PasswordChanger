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
        /// Creates and exports a CSV file from a DataTable.
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
                        "OpenOrder Password",
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
                    items.Add(QuoteValue(openOrderUser.Password));
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
    }
}
