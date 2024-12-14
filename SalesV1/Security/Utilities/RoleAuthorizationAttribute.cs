using System;
using System.Web.Mvc;
using Security.Models;
using Security.Services;

namespace Security.Utilities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RoleAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizationAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            // Simulación: Obtener el usuario autenticado desde la sesión
            var user = (User)httpContext.Session["CurrentUser"];
            if (user == null)
                return false;

            // Validar si el usuario tiene alguno de los roles permitidos
            var authService = new AuthorizationService();
            return authService.IsAuthorized(user, _roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirigir a una página de acceso denegado o retornar un código HTTP 403
            filterContext.Result = new HttpStatusCodeResult(403, "No autorizado");
        }
    }
}
