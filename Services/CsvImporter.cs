using Covid.Data;
using Covid.Models;
using CsvHelper;
using CsvHelper.Configuration;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Services
{
    public interface ICsvImporter
    {
        public Task<string> ImportDailyCounts(ApplicationDbContext context);
    }

    public class CsvImporter : ICsvImporter
    {
        public async Task<string> ImportDailyCounts(ApplicationDbContext context)
        {
            using (var reader = new StreamReader(@"C:\Users\Ryan\source\repos\Covid\Data\us_counties_covid19_daily.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<DailyCountMap>();
                var dailyCounts = csv.GetRecords<DailyCount>().ToList();

                try
                {
                    context.Database.SetCommandTimeout(180);
                    context.ChangeTracker.AutoDetectChangesEnabled = false;
                    await context.BulkInsertAsync(dailyCounts);
                }
                catch (Exception ex)
                {
                    return "Import Unsuccessful! Error: " + ex.Message;
                }
                finally
                {
                    context.ChangeTracker.AutoDetectChangesEnabled = true;
                }

                return "DailyCount Import Successful!";
            }
        }
    }

    public class DailyCountMap : ClassMap<DailyCount>
    {
        public DailyCountMap()
        {
            Map(m => m.Date).Name("date");
            Map(m => m.County).Name("county");
            Map(m => m.State).Name("state");
            Map(m => m.Fips).Name("fips");
            Map(m => m.Cases).Name("cases");
            Map(m => m.Deaths).Name("deaths");
        }
    }
}
