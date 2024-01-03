using Admin.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Service.IService
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfoDto>> GetUserDetailsAsync();
    }
}
