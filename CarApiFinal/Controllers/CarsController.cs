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

        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _carService.GetCars();
            return Ok(cars);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCarById(int id)
        {
            var cars = _carService.GetCars();
            var car = cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            return Ok(car);
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

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCarById(int id)
        {
            var cars = _carService.GetCars();
            var car = cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            cars.Remove(car);
            _carService.SaveCars(cars);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCarById(int id, [FromBody] Car updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("Car data is invalid.");
            }

            var cars = _carService.GetCars();
            var existingCar = cars.FirstOrDefault(c => c.Id == id);

            if (existingCar == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            existingCar.Make = updatedCar.Make;
            existingCar.Model = updatedCar.Model;
            existingCar.Year = updatedCar.Year;

            _carService.SaveCars(cars);

            return Ok(existingCar);
        }


    }
}