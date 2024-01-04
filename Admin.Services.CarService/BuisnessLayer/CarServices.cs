using Admin.Services.CarService.DataLayer.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.CarService.BuisnessLayer
{
    public class CarServices:ICarServices
    {

        private readonly DealerApisemiFinalContext finalContext;
        private readonly IMapper _mapper;
        public CarServices(DealerApisemiFinalContext _finalContext, IMapper mapper)
        {
            finalContext = _finalContext;
            _mapper = mapper;
        }


        public async Task<string> AddCar(CarDto car)
        {
            try
            {
                if (car == null)
                {
                    return "Invalid car data";
                }

                var newCar = _mapper.Map<Car>(car);
                finalContext.Cars.Add(newCar);
                await finalContext.SaveChangesAsync();

                return "Car added successfully";
            }
            catch (Exception)
            {
                return "Error adding car to the database";
            }
        }



        public async Task<IEnumerable<CarDto>> GetAllCars()
        {
            try
            {
                var cars = await finalContext.Cars.ToListAsync();

                if (cars != null)
                {
                    var Allcars = _mapper.Map<IEnumerable<CarDto>>(cars);
                    return Allcars;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<CarDto> GetCarById(int id)
        {
            try
            {
                var carsdetails = await finalContext.Cars.FindAsync(id);
                if (carsdetails != null)
                {
                    var car = _mapper.Map<CarDto>(carsdetails);
                    return car;
                }

                Console.WriteLine("Not found");
                return null;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateCar(int id, CarDto updatedCar)
        {
            try
            {
                var existingCar = await finalContext.Cars.FindAsync(id);

                if (existingCar != null)
                {
                    _mapper.Map(updatedCar, existingCar);

                    finalContext.Cars.Update(existingCar);
                    await finalContext.SaveChangesAsync();

                    return true; // Successfully updated
                }
                else
                {
                    return false; // Car not found for update
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"An error occurred during update: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCar(int id)
        {
            try
            {
                var carToDelete = await finalContext.Cars.FindAsync(id);

                if (carToDelete != null)
                {
                    finalContext.Cars.Remove(carToDelete);
                    await finalContext.SaveChangesAsync();
                    return true; // Successfully deleted
                }
                else
                {
                    return false; // Car not found for deletion
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"An error occurred during deletion: {ex.Message}");
                return false;
            }
        }

    }
}
