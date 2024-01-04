using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

public class UserInfoController : Controller
{
    private readonly IUserInfoService _userInfoService;
    private readonly IStateService _stateService;

    public UserInfoController(IUserInfoService userInfoService, IStateService stateService)
    {
        _userInfoService = userInfoService;
        _stateService = stateService;
    }

    public async Task<ActionResult> UserInfoIndex()
    {
        try
        {
            var result = await _userInfoService.GetUserDetailsAsync();

            if (result != null)
            {
                // Assuming you have a method to get the states, replace it with your actual logic
           

                var states = await _stateService.GetStateDetailsAsync();
                ViewBag.States = states ?? new List<StateDto>(); // Null check

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

    public async Task<ActionResult> UserInfoDelete(int userid)
    {
        if (ModelState.IsValid)
        {
            var result = await _userInfoService.DeleteUserAsync(userid);

            if (result != null)
            {
                return RedirectToAction(nameof(UserInfoIndex));
            }
            else
            {
                // Handle 404 Not Found
                ModelState.AddModelError(string.Empty, "The requested state was not found.");
                return RedirectToAction(nameof(UserInfoIndex));
            }
        }
        return NotFound();
    }

    public async Task<ActionResult> UserToUpdate(int userid)
    {
        try
        {
            var states = await _stateService.GetStateDetailsAsync();
            ViewBag.States = states ?? new List<StateDto>(); // Null check
            ViewBag.StatesCount = ViewBag.States.Count;

            var existingState = await _userInfoService.GetUserByIdAsync(userid);

            if (existingState != null)
            {
                return View("UserUpdate", existingState); // Pass existing state to the view
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"State with ID {userid} not found");
                return RedirectToAction(nameof(UserInfoIndex));
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as appropriate for your application
            ModelState.AddModelError(string.Empty, "Internal Server Error");
            return RedirectToAction(nameof(UserInfoIndex));
        }
    }

    [HttpPost]
    public async Task<ActionResult> UserUpdate([FromRoute] int userid, [FromForm] UserInfoDto updatedUserDto)
    {
        try
        {
            var states = await _stateService.GetStateDetailsAsync();
            ViewBag.States = states ?? new List<StateDto>(); // Null check
            ViewBag.StatesCount = ViewBag.States.Count;

            if (ModelState.IsValid)
            {
                userid = updatedUserDto.Id;
                var updatedState = await _userInfoService.UpdateUserAsync(userid, updatedUserDto);

                if (updatedState != null)
                {
                    return RedirectToAction(nameof(UserInfoIndex));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"User with ID {userid} not found");
                    return View("UserUpdate", updatedUserDto);
                }
            }

            return View("UserUpdate", updatedUserDto);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as appropriate for your application
            ModelState.AddModelError(string.Empty, "Internal Server Error");
            return View("UserUpdate", updatedUserDto);
        }
    }

}
