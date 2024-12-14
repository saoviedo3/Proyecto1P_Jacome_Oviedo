using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;
using WebApp.SecurityGUI.Models;

namespace WebApp.SecurityGUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext _context;

        public RolesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Index
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            var users = _context.Users.ToList();

            ViewBag.Users = users;

            return View(roles);
        }

        // GET: Crear rol
        [HttpGet]
        public ActionResult Create()
        {
            return View(new RoleViewModel());
        }

        // POST: Crear rol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Roles.Any(r => r.Name == model.RoleName))
                {
                    _context.Roles.Add(new IdentityRole { Name = model.RoleName });
                    _context.SaveChanges();
                    TempData["Success"] = "Rol creado correctamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "El rol ya existe.");
            }

            return View(model);
        }

        // Vista para asignar roles (igual que antes)
        public ActionResult AssignRole()
        {
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();

            var viewModel = new AssignRoleViewModel
            {
                Users = users.Select(u => new System.Web.Mvc.SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                }).ToList(),

                Roles = roles.Select(r => new System.Web.Mvc.SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole(AssignRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.RoleName))
            {
                ModelState.AddModelError("", "Debe seleccionar un usuario y un rol.");
                return RedirectToAction("Index");
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            if (userManager.IsInRole(model.UserId, model.RoleName))
            {
                ModelState.AddModelError("", "El usuario ya tiene este rol asignado.");
                return RedirectToAction("Index");
            }

            userManager.AddToRole(model.UserId, model.RoleName);

            TempData["Success"] = "Rol asignado correctamente.";
            return RedirectToAction("Index");
        }

    }
}
