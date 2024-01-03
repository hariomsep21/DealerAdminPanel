using Admin.Services.UserinfoAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.UserinfoAPI.BusinessLayer.IService
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<UsersDto>>> GetUserDetailsAsync();
    }
}
