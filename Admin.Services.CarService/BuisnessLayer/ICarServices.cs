namespace Admin.Services.CarService.BuisnessLayer
{
    public interface ICarServices
    {
        Task<string> AddCar(CarDto car);
        Task<IEnumerable<CarDto>> GetAllCars();
        Task<CarDto> GetCarById(int id);
        Task<bool> DeleteCar(int id);
        Task<bool> UpdateCar(int id, CarDto updatedCar);
    }
}
