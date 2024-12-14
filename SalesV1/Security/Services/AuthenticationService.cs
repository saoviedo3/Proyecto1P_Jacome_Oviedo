using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Models;
using DAL;
using System;

namespace Security.Services
{
    public class AuthenticationService
    {
        public User Login(string username, string password)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                var user = repository.Retrieve<User>(u => u.Username == username && u.IsActive);
                if (user == null)
                    throw new Exception("Usuario no encontrado o inactivo.");

                if (!VerifyPassword(password, user.PasswordHash))
                    throw new Exception("Contraseña incorrecta.");

                return user;
            }
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
