using Admin.UI.Models;
using Admin.UI.Service;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Admin.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult LoginIndex()
        {

            return View();
        }

        public async Task<ActionResult> AdminCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdminLoginAsync(AdminDto model)
        {
            try
            {
                // Check if the username exists
                var existingAdmin = await _adminService.GetAdminDetailsAsync();

                if (existingAdmin != null)
                {
                    // Username exists, try to log in
                    var result = await _adminService.LoginAdmin(model);

                    if (result != null)
                    {
                        TempData["successMessage"] = "Admin login successful.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["dangerMessage"] = "Admin login failed. Please try again.";
                    }
                }
                else
                {
                    TempData["dangerMessage"] = "Admin with this username does not exist.";
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                TempData["dangerMessage"] = "An error occurred while processing your request. Please try again.";
            }

            // If ModelState is not valid or the login fails, return to the view
            return View("Index", model);
        }



        [HttpPost]
        public async Task<ActionResult> AdminCreateAsync(AdminDto model)
        {
            var adminList = await _adminService.GetAdminDetailsAsync();
            var existingAdmin = adminList.FirstOrDefault(e => e.UserName == model.UserName);

            if (existingAdmin == null)
            {
                if (ModelState.IsValid)
                {
                    var result = await _adminService.RegisterAdmin(model);

                    if (result != null)
                    {
                        TempData["successMessage"] = "Admin registration successful.";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["dangerMessage"] = "Admin registration failed. Please try again.";
                }
            }
            else
            {
               // ModelState.AddModelError("UserName", "Admin with this username already exists.");
                TempData["dangerMessage"] = "Admin registration failed. Please try again.";
                return View("Index", model);
            }

            // If ModelState is not valid or the admin already exists, return to the view
            return View("Index", model);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Log the error
            _logger.LogError("An error occurred: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
