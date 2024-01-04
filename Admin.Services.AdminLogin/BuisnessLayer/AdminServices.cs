using AdminService.DataLayer.Models;
using AdminService.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace AdminService.BuisnessLayer
{
    public class AdminServices:IAdminServices
    {

        private readonly DealerApifinalContext _context;
        private readonly IConfiguration _config;

        public AdminServices(DealerApifinalContext dealerApifinalContext, IConfiguration configuration)
        {
            _context = dealerApifinalContext;
            _config = configuration;
        }

        public async Task<IActionResult> RegisterAdmin(AdminDto adminDTO)
        {
            try
            {
                CreatePasswordHash(adminDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var admin = new AdminTable
                {
                    UserName = adminDTO.UserName,
                    PasswordHast = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.AdminTables.Add(admin);
                await _context.SaveChangesAsync();

                return new CreatedAtActionResult(nameof(RegisterAdmin), "Admin", new { id = admin.Id }, admin);
            }
            catch (DbUpdateException )
            {
                return new StatusCodeResult(500); // Consider returning a specific error message
            }
            catch (Exception )
            {
                return new StatusCodeResult(500); // Consider returning a specific error message
            }
        }
        private void CreatePasswordHash(string Password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }


        public async Task<IActionResult> LoginAdmin(AdminDto adminDTO)
        {
            try
            {
                var adminUser = await _context.AdminTables.FirstOrDefaultAsync(p => p.UserName == adminDTO.UserName);
                if (adminUser == null)
                {
                    return new UnauthorizedResult();
                }

                byte[] enteredPasswordHash, enteredPasswordSalt;
                CreatePasswordHash(adminDTO.Password, adminUser.PasswordSalt, out enteredPasswordHash, out enteredPasswordSalt);

                if (!enteredPasswordHash.SequenceEqual(adminUser.PasswordHast))
                {
                    return new UnauthorizedResult();
                }

                string token = CreateToken(adminUser);
                return new OkObjectResult(new { Token = token });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        // Other methods...

        private void CreatePasswordHash(string password, byte[] storedSalt, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSalt = storedSalt;
            }
        }

            private string CreateToken(AdminTable admininfo)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admininfo.Id.ToString()),
                new Claim(ClaimTypes.Name, admininfo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

