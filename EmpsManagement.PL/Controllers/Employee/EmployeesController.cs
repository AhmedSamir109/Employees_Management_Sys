using EmpsManagement.BLL.DTOs.Employee;
using EmpsManagement.BLL.Services;
using EmpsManagement.BLL.Services.Employee;
using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using EmpsManagement.BLL.Services.Attachment;

namespace EmpsManagement.PL.Controllers.Employee
{

    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeesController(IEmployeeServices employeeServices, ILogger<EmployeesController> logger, IWebHostEnvironment environment)
        {
            _employeeServices = employeeServices;
            _logger = logger;
            _environment = environment;
        }
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeServices.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }


        #region Create

        [HttpGet]
        public IActionResult Create([FromServices] IDepartmentServices departmentServices)
        {
            var departments = departmentServices.GetAllDepartments();
            ViewBag.Departments = departments;

            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employee)
        {
            if (ModelState.IsValid) // server-side validation
            {
                try
                {
                    int result = _employeeServices.createEmployee(employee);
                    string message;

                    if (result > 0)
                    {
                        message = $"Employee {employee.Name} created successfully.";
                    }
                    else
                    {
                        message = $"An error occurred while creating the Employee.";
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
            return View(employee);
        }


        #endregion

        #region Get Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = _employeeServices.GetbyId(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id, [FromServices] IDepartmentServices departmentServices)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var departments = departmentServices.GetAllDepartments();
            ViewBag.Departments = departments;

            var employee = _employeeServices.GetbyId(id.Value);


            if (employee == null)
            {
                return NotFound();
            }

            var updateEmployeeDto = new UpdateEmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                Gender = Enum.Parse<Gender>(employee.Gender),
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                ImageName = employee.ImageName
            };


            return View(updateEmployeeDto);

        }


        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto employee)
        {
            if (!id.HasValue || id != employee.Id) return BadRequest();

            if (!ModelState.IsValid) return View(employee);


            try
            {
                int result = _employeeServices.UpdateEmployee(employee , employee.ImageName );
                
                string message;
                if (result > 0)
                {
                    message = $"Department {employee.Name} is Edited successfully.";
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

            return View(employee);

        }

        #endregion

        #region Delete
        public IActionResult Delete(int id, [FromServices] IAttachmentServices attachmentServices)
        {
            if (id == 0)
                return BadRequest();

            var employee = _employeeServices.GetbyId(id);
            bool Deleted = _employeeServices.DeleteEmployee(id);

            if (Deleted)
            {
                attachmentServices.Delete(employee.ImageName, "images");
                return RedirectToAction("Index");
            }

            // Return a default response if deletion fails
            ModelState.AddModelError("", "An error occurred while deleting the department.");
            return View("Error");
        }
        #endregion
    }
}
