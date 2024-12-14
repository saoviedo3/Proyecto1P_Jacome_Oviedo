using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Security.Utilities
{
    public static class PasswordUtils
    {
        public static string GenerateStrongPassword()
        {
            // Implementa la lógica para generar contraseñas seguras.
            return Guid.NewGuid().ToString().Substring(0, 8); // Ejemplo simplificado
        }
    }
}
