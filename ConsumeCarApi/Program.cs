using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string baseUrl = "https://localhost:5048/";
        string endpoint = "/api/cars";

        using HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

        try
        {
            var cars = await client.GetFromJsonAsync<Car[]>(endpoint);

            if (cars != null)
            {
                Console.WriteLine("Cars from API:");
                foreach (var car in cars)
                {
                    Console.WriteLine($"ID: {car.Id}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}");
                }
            }
            else
            {
                Console.WriteLine("No cars found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}