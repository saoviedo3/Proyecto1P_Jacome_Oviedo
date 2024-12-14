using System.Collections.Generic;

namespace WebApp.SecurityGUI.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; } // ID del usuario seleccionado
        public string RoleName { get; set; } // Nombre del rol seleccionado

        // Para mostrar la lista de usuarios y roles en los desplegables
        public List<System.Web.Mvc.SelectListItem> Users { get; set; }
        public List<System.Web.Mvc.SelectListItem> Roles { get; set; }
    }
}
