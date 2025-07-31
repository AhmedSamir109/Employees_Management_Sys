using EmpsManagement.DAL.Models.Identity;
using EmpsManagement.PL.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmpsManagement.PL.Controllers.User
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        #region GetAll

        [HttpGet]
        public async Task<IActionResult> Index(string searchWord)
        {
            if (string.IsNullOrEmpty(searchWord))
            {
                var usersList = await _userManager.Users.ToListAsync();

                var users = new List<UserViewModel>();

                foreach (var U in usersList)
                {
                    var roles = await _userManager.GetRolesAsync(U);
                    if (roles.Count == 0)
                    {
                        roles.Add("No Roles Assigned");
                    }
                    users.Add(new UserViewModel
                    {
                        Id = U.Id,
                        FName = U.FirstName,
                        LName = U.LastName ?? "_",
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber ?? "_",
                        Roles = roles
                    });
                }

                return View(users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(searchWord);

                if (user == null)
                {
                    return View(new List<UserViewModel>());
                }

                var roles = await _userManager.GetRolesAsync(user);

                var userVM = new UserViewModel
                {
                    Id = user.Id,
                    FName = user.FirstName,
                    LName = user.LastName ?? "_",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber ?? "_",
                    Roles = roles
                };

                return View(new List<UserViewModel>() { userVM });
            }
        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(string? Id, string viewName = "Details")
        {
            if (Id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(Id);

            if (User is null)
            {
                return NotFound();
            }
            else
            {
                var UserVm = new UserViewModel
                {
                    Id = user.Id,
                    FName = user.FirstName,
                    LName = user.LastName ?? "_",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber ?? "_",
                    Roles = _userManager.GetRolesAsync(user).Result
                };

                return View(viewName, UserVm);
            }


        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {

                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    var roles = await _userManager.GetRolesAsync(user);


                    if (user == null)
                    {
                        ModelState.AddModelError("", "User not found.");
                        return View(model);
                    }

                    user.FirstName = model.FName;
                    user.LastName = model.LName;
                    user.PhoneNumber= model.PhoneNumber;
                    
                    
                    //await _userManager.AddToRoleAsync(user , model.Roles?.FirstOrDefault() ?? "No Roles Assigned");
                    await _roleManager.CreateAsync(new IdentityRole(model.Roles?.FirstOrDefault() ?? "No Roles Assigned"));


                    await _userManager.UpdateAsync(user);
                    TempData["Message"] = "User Updated Successfully";

                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating user: {ex.Message}");
                }


            }

            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(id);
              
                if(user is not null)
                    await _userManager.DeleteAsync(user);
                TempData["Message"] = "User Deleted Successfully";


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Message"] = ex.Message;

            }
                return RedirectToAction("Index");

        }

        #endregion






    }
}
