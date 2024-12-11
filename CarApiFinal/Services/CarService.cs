using System.Text.Json;
using CarApiFinal.Models;
using System.IO;

namespace CarApiFinal.Services
{
    public class CarService
    {
        private readonly string filePath;

        public CarService()
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "cars.json");
        }

        public List<Car> GetCars()
        {
            Console.WriteLine("GetCars called");
            Console.WriteLine($"File path: {filePath}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("cars.json does not exist. Returning an empty list.");
                return new List<Car>();
            }

            var json = File.ReadAllText(filePath);
            Console.WriteLine($"cars.json content before deserialization: {json}");

            return JsonSerializer.Deserialize<List<Car>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Car>();
        }



        public void SaveCars(List<Car> cars)
        {
            var json = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

    }
}