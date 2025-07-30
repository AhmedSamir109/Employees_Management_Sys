using AspNetCoreGeneratedDocument;
using EmpsManagement.BLL.DTOs.Department;
using EmpsManagement.BLL.Services;
using EmpsManagement.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmpsManagement.PL.Controllers.Department
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentsController(IDepartmentServices departmentServices, ILogger<DepartmentsController> logger, IWebHostEnvironment environment)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _environment = environment;
        }

        public ActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }


        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDepartmentDto department)
        {
            if (ModelState.IsValid)                          //server side validation 
            {
                try
                {
                    int Result = _departmentServices.createDepartment(department);
                    string message;
                    if (Result > 0)
                    {
                        message = $"Department {department.Name} created successfully.";
                    }
                    else
                    {
                        message = $"An error occurred while creating the department.";
                    }

                    TempData["Message"] = message;
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(department);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View(department);

                    }
                }
            }
            else
            {
                return View(department);

            }

        }



        #endregion

        #region Get Details

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentServices.GetbyId(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentServices.GetbyId(id.Value);
            if (department == null) return NotFound();

            var departmentViewModel = new DepartmentEditViewModel
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn

            };

            return View(departmentViewModel);
        }


        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, DepartmentEditViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartment = new UpdateDepartmentDto
                    {
                        Id = Id,
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };

                    int result = _departmentServices.UpdateDepartment(updatedDepartment);
                    string message;
                    if (result > 0)
                    {
                        message = $"Department {departmentViewModel.Name} is Edited successfully.";
                    }
                    else
                    {
                        message = $"An error occurred while Editing the department.";
                    }

                    TempData["Message"] = message;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("Error", ex);
                    }
                }

            }

            return View(departmentViewModel);

        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            bool Deleted = _departmentServices.DeleteDepartment(id);

            if (Deleted)
                return RedirectToAction("Index");

            // Return a default response if deletion fails
            ModelState.AddModelError("", "An error occurred while deleting the department.");
            return View("Error");
        }

        #endregion


    }
}

