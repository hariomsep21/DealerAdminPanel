using Admin.Services.CarService.BuisnessLayer;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    public class CarController:ControllerBase
    {

        private readonly ICarServices _carservice;

        public CarController(ICarServices carservice)
        {
            _carservice = carservice;
        }



        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] CarDto car)
        {
            var result = await _carservice.AddCar(car);

            if (result.Equals("Car added successfully"))
            {
                return Ok(result); // Return 200 OK status
            }
            else if (result.Equals("Invalid car data"))
            {
                return BadRequest(result); // Return 400 Bad Request status
            }
            else
            {
                return StatusCode(500, result); // Return 500 Internal Server Error status
            }
        }



        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carservice.GetAllCars();
            if (cars != null)
            {
                return Ok(cars); // Return list of CarDto objects with 200 OK status
            }
            else
            {
                return StatusCode(500, "Failed to retrieve cars"); // Return 500 Internal Server Error status
            }
        }




        [HttpGet("GetCarById")]
        public async Task<IActionResult> GetCarById(int id)
        { 
            var cars = await _carservice.GetCarById(id);
            if (cars != null)
            {
                return Ok(cars); // Return list of CarDto objects with 200 OK status
            }   
        else
            {
                return StatusCode(500, "Failed to retrieve cars"); // Return 500 Internal Server Error status
            }
        }



        [HttpPut("UpdateCar")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody]CarDto updatedCar)
        {
            var result = await _carservice.UpdateCar(id,updatedCar);
            if (result)
            {
                return Ok("Car updated successfully");
            }
            else
            {
                return NotFound("Car not found for update");
            }
        }



        [HttpDelete("DeleteCar/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carservice.DeleteCar(id);
            if (result)
            {
                return Ok("Car deleted successfully");
            }
            else
            {
                return NotFound("Car not found for deletion");
            }
        }
    }




    }

