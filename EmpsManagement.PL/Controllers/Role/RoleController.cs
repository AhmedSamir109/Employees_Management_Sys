using EmpsManagement.PL.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmpsManagement.PL.Controllers.Role
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index(string searchWord)
        {
            if (string.IsNullOrEmpty(searchWord))
            {
                var Roles = _roleManager.Roles.ToList();
                var RolesViewModel = Roles.Select(r => new RoleViewModel { Id = r.Id, RoleName = r.Name! }).ToList();
                return View(RolesViewModel);
            }

            var role = _roleManager.FindByNameAsync(searchWord).Result;
            var roleViewModel = new RoleViewModel { Id = role.Id, RoleName = role.Name! };
            return View(new List<RoleViewModel> { roleViewModel });
        }


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Id = roleViewModel.Id, Name = roleViewModel.RoleName };
                _roleManager.CreateAsync(role).Wait();
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        #endregion


        #region Edit

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }
            var roleViewModel = new RoleViewModel { Id = role.Id, RoleName = role.Name! };
            return View(roleViewModel);
        }


        [HttpPost]
        public IActionResult Edit(RoleViewModel roleViewModel, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = roleViewModel.RoleName;
                _roleManager.UpdateAsync(role).Wait();
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        #endregion


        #region Delete

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }
            _roleManager.DeleteAsync(role).Wait();
            return RedirectToAction("Index");
        }

        #endregion

    }

}
