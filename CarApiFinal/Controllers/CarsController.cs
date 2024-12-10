using Microsoft.AspNetCore.Mvc;
using CarApiFinal.Models;
using CarApiFinal.Services;

namespace CarApiFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody] Car newCar)
        {
            if (newCar == null)
            {
                return BadRequest("Car data is invalid.");
            }

            var cars = _carService.GetCars();
            newCar.Id = cars.Count > 0 ? cars.Max(c => c.Id) + 1 : 1;
            cars.Add(newCar);
            _carService.SaveCars(cars);

            return CreatedAtAction(nameof(CreateCar), new { id = newCar.Id }, newCar);
        }
    }
}