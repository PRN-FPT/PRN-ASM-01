using ASM_01.BusinessLayer.DTOs;
using ASM_01.BusinessLayer.Services.Abstract;
using ASM_01.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASM_01.BusinessLayer.Services
{
    public class SimpleAuthService(EVRetailsDbContext _db) : ISimpleAuthService
    {
        /// <summary>
        /// Login method that validates username and password and returns an AuthDto if successful.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>AuthDto with Username = username; Role = username.ToUpper()</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public AuthDto Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || user.PasswordHash != HashPassword(password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (user.Role == "DEALER")
            {
                var dealer = _db.Dealers.FirstOrDefault(d => d.UserId == user.UserId)!;
                return new AuthDto
                {
                    Id = dealer.DealerId,
                    Username = username,
                    Role = username.ToUpper()
                };
            }
            else if (user.Role == "DISTRIBUTOR")
            {
                return new AuthDto
                {
                    Id = user.UserId,
                    Username = username,
                    Role = username.ToUpper()
                };
            }
            throw new UnauthorizedAccessException("User role not recognized.");
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
