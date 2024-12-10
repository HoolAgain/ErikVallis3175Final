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
            if (!File.Exists(filePath))
            {
                return new List<Car>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
        }

        public void SaveCars(List<Car> cars)
        {
            var json = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}