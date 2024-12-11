using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string baseUrl = "http://localhost:5048/";
        string endpoint = "api/cars";

        using HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

        while (true)
        {
            Console.WriteLine("\nCar API Console Application\n\n1. Read all cars\n2. Read a specific car by ID\n3. Create a new car\n4. Update a car by ID\n5. Delete a car by ID\n6. Exit\n\nChoose an option (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    await ReadAllCars(client, endpoint);
                    break;
                case "2":
                    await ReadCarById(client, endpoint);
                    break;
                case "3":
                    await CreateCar(client, endpoint);
                    break;
                case "4":
                    await UpdateCarById(client, endpoint);
                    break;
                case "5":
                    await DeleteCarById(client, endpoint);
                    break;
                case "6":
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }

    static async Task ReadAllCars(HttpClient client, string endpoint)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<Car>>(responseBody);

            if (cars != null && cars.Count > 0)
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

    static async Task ReadCarById(HttpClient client, string endpoint)
    {
        Console.Write("Enter the ID of the car: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{endpoint}/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var car = JsonConvert.DeserializeObject<Car>(responseBody);

                if (car != null)
                {
                    Console.WriteLine($"ID: {car.Id}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}");
                }
                else
                {
                    Console.WriteLine("Car not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static async Task CreateCar(HttpClient client, string endpoint)
    {
        Console.Write("Enter the make of the car: ");
        string make = Console.ReadLine();

        Console.Write("Enter the model of the car: ");
        string model = Console.ReadLine();

        Console.Write("Enter the year of the car: ");
        string year = Console.ReadLine();

        var newCar = new Car { Make = make, Model = model, Year = year };

        try
        {
            string jsonBody = JsonConvert.SerializeObject(newCar);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var createdCar = JsonConvert.DeserializeObject<Car>(responseBody);

            Console.WriteLine($"Car created successfully: ID: {createdCar.Id}, Make: {createdCar.Make}, Model: {createdCar.Model}, Year: {createdCar.Year}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task UpdateCarById(HttpClient client, string endpoint)
    {
        Console.Write("Enter the ID of the car to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Enter the new make of the car: ");
            string make = Console.ReadLine();

            Console.Write("Enter the new model of the car: ");
            string model = Console.ReadLine();

            Console.Write("Enter the new year of the car: ");
            string year = Console.ReadLine();

            var updatedCar = new Car { Make = make, Model = model, Year = year };

            try
            {
                string jsonBody = JsonConvert.SerializeObject(updatedCar);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{endpoint}/{id}", content);
                response.EnsureSuccessStatusCode();

                Console.WriteLine("Car updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static async Task DeleteCarById(HttpClient client, string endpoint)
    {
        Console.Write("Enter the ID of the car to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{endpoint}/{id}");
                response.EnsureSuccessStatusCode();

                Console.WriteLine("Car deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}

public class Car
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("make")]
    public string Make { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("year")]
    public string Year { get; set; }
}
