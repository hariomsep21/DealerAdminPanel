using Admin.Services.UserinfoAPI.BusinessLayer.IService;
using Admin.Services.UserinfoAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Services.UserinfoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserInfoAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private ResponseDto _response;

        public UserInfoAPIController(IUserService userService)
        {
            _userService = userService;
            _response = new ResponseDto();
        }

        [HttpGet("Getdetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserInfoSupport()
        {
            try
            {
                // Call the GetUserDetailsAsync method from the UserService
                var result = await _userService.GetUserDetailsAsync();

                // Return the result from the service
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }
    }
}



