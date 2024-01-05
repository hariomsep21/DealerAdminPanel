using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class CarController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ICarService _carService;

        public CarController(IUserInfoService userInfoService, ICarService carService)
        {
            _userInfoService = userInfoService;
            _carService = carService;
        }

        public async Task<ActionResult> CarIndex()
        {
            try
            {
                var result = await _carService.GetCarDetailsAsync();

                if (result != null)
                {
                    // Assuming you have a method to get the states, replace it with your actual logic


                    var states = await _userInfoService.GetUserDetailsAsync();
                    ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                    ViewBag.StatesCount = result.Count();

                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<UserInfoDto>());
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw; // rethrow the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // rethrow the exception to propagate it up the call stack
            }

        }

        public async Task<ActionResult> CarCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CarCreate(CarDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.AddCarAsync(model);

                if (result != null)
                {
                    return RedirectToAction(nameof(CarIndex));
                }
            }
            return View(model);
        }


        public async Task<ActionResult> CarDelete(int carId)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.DeleteCarAsync(carId);

                if (result != null)
                {
                    return RedirectToAction(nameof(CarIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    return RedirectToAction(nameof(CarIndex));
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> CarToUpdate(int userid)
        {
            try
            {
                var states = await _userInfoService.GetUserDetailsAsync();
                ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                ViewBag.StatesCount = ViewBag.States.Count;

                var existingState = await _carService.GetCarByIdAsync(userid);


                var StoreUser = existingState.UserId;
                ViewBag.StoreUser = StoreUser;



                if (existingState != null)
                {
                    return View("UpdateCar", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {userid} not found");
                    return RedirectToAction(nameof(CarIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(CarIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCar(int id, CarDto updatedCarDto)
        {
            try
            {
                //var states = await _userInfoService.GetUserDetailsAsync();
                //ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                //ViewBag.StatesCount = ViewBag.States.Count;

                if (ModelState.IsValid)
                {
                    id = updatedCarDto.CarId;
                    var updatedState = await _carService.UpdateCarAsync(id, updatedCarDto);

                    if (updatedState != null)
                    {
                        return RedirectToAction(nameof(CarIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"User with ID {id} not found");
                        return View("UpdateCar", updatedCarDto);
                    }
                }

                return View("UpdateCar", updatedCarDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("UpdateCar", updatedCarDto);
            }
        }
    }
}
