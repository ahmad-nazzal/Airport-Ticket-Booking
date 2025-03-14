using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Airport_Ticket_Booking.Infrastructure
{
    public class CsvHandler<T>
    {
        private readonly string _filePath;
        public CsvHandler(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }
        public List<T> ReadFromCsv()
        {
            try
            {
                if (new FileInfo(_filePath).Length == 0) return new List<T>();

                using var reader = new StreamReader(_filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<T>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                return new List<T>();
            }
        }
        public void WriteToCsv(List<T> records)
        {
            try
            {
                using var writer = new StreamWriter(_filePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(records);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV file: {ex.Message}");
            }
        }


    }
}
