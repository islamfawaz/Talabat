using CsvHelper;
using Route.Talabat.Core.Domain.Entities.Food;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Persistence.Services
{
    public class CsvDataLoader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CsvDataLoader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<FoodItem> LoadData(string fileName)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found at: {filePath}");
            }

            using var reader = new StreamReader(filePath);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using var csv = new CsvReader(reader, config);

            return csv.GetRecords<FoodItem>().ToList();
        }
    }
}
