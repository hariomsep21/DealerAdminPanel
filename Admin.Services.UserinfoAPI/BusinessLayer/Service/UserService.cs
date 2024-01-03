using Admin.Services.UserinfoAPI.BusinessLayer.IService;
using Admin.Services.UserinfoAPI.Models;
using Admin.Services.UserinfoAPI.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Services.UserinfoAPI.BusinessLayer.Service
{
    
    public class UserService : IUserService
    {
        private readonly DealerApifinalContext _db;
        private readonly IMapper _mapper;

        public UserService(DealerApifinalContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserDetailsAsync()
        {
            try
            {
                var userInfoDetails = await _db.Userstbls.ToListAsync();

                // Use AutoMapper to map the entities to DTO
                var userInfoDTO = _mapper.Map<IEnumerable<UsersDto>>(userInfoDetails);

                // Return 200 OK with the DTO
                return new OkObjectResult(userInfoDTO);
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
