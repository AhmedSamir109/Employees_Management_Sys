using EmpsManagement.BLL.DTOs.Employee;
using EmpsManagement.BLL.Factories;
using EmpsManagement.BLL.Services.Attachment;
using EmpsManagement.DAL.Repositories.Classes;
using EmpsManagement.DAL.Repositories.Interfaces;
using EmpsManagement.DAL.Unit_Of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.Services.Employee
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentServices _attachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork , IAttachmentServices attachmentServices)
        {
            _unitOfWork = unitOfWork;
            _attachmentServices = attachmentServices;
        }

        #region GetAll
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {

            //IEnumerable<EmployeeDto> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                return _unitOfWork.EmployeeRepository.GetAll().Select(E => E.ToEmployeeDto());
            }
            else
            {
                return _unitOfWork.EmployeeRepository.GetAll( E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()))
                                          .Select(E=>E.ToEmployeeDto());
            }

        }

        #endregion

        #region GetById
        public EmployeeDetailsDto? GetbyId(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : employee.ToEmployeeDetailsDto();
        }
        #endregion

        #region Create
        public int createEmployee(CreateEmployeeDto employeeDto)
        {
            string? ImageName;
            if (employeeDto.Image is not null) 
            {
                ImageName = _attachmentServices.Upload(employeeDto.Image, "images");
            }else
            {
                ImageName = null; // If no image is provided, set ImageName to null
            }

            _unitOfWork.EmployeeRepository.Add(employeeDto.ToEntity(ImageName));
            return _unitOfWork.SaveChanges();
        }
        #endregion

        #region Update 
        public int UpdateEmployee(UpdateEmployeeDto employeeDto , string? oldImageName)
        {
            string? ImageName;

            if (employeeDto.Image is not null)
            {
                ImageName = _attachmentServices.Upload(employeeDto.Image, "images");
                _attachmentServices.Delete(oldImageName, "images");
            }else
            {
                ImageName = oldImageName; // If no new image is provided, keep the old image name
            }

            _unitOfWork.EmployeeRepository.Update(employeeDto.ToEntity(ImageName));
            return _unitOfWork.SaveChanges();
        }
        #endregion

        #region Delete
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null)
            {
                return false; // Employee not found
            }
            _unitOfWork.EmployeeRepository.Remove(employee) ;
            return _unitOfWork.SaveChanges() > 0 ? true : false;            // returns true if the delete operation was successful

        }
        #endregion



    }
}
