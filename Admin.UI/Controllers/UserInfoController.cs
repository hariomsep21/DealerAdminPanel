using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

public class UserInfoController : Controller
{
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    public async Task<ActionResult> UserInfoIndex()
    {
        try
        {
            var result = await _userInfoService.GetUserDetailsAsync();

            if (result != null)
            {
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
}
