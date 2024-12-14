using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Ejemplo: Admin, Editor, Viewer
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}