using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Models;
using System;

namespace Security.Services
{
    public class AuthorizationService
    {
        // Validar si el usuario tiene el rol requerido
        public bool IsAuthorized(User user, string requiredRole)
        {
            if (user == null)
                throw new Exception("Usuario no autenticado.");

            return user.Role == requiredRole;
        }

        // Validar múltiples roles permitidos
        public bool IsAuthorized(User user, string[] allowedRoles)
        {
            if (user == null)
                throw new Exception("Usuario no autenticado.");

            foreach (var role in allowedRoles)
            {
                if (user.Role == role)
                    return true;
            }

            return false;
        }
    }
}


