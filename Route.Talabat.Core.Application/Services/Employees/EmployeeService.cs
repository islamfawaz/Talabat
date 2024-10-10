using AutoMapper;
using Route.Talabat.Application.Abstraction.Employee;
using Route.Talabat.Application.Abstraction.Employee.Models;
using Route.Talabat.Core.Domain.Contract.Persistence;
using Route.Talabat.Core.Domain.Entities.Employees;
using Route.Talabat.Core.Domain.Specifications.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService : IEmployeeService

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync()
        {
            var spec = new EmployeeWithDepartmentSpecifications();
            var employees =await unitOfWork.GetRepository<Employee, int>().GetAllAsyncWithSpec(spec);
            var employeesToReturn=mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);

            return employeesToReturn;
        }

        public async Task<EmployeeToReturnDto> GetEmployeeByIdAsync(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);
            var employee = await unitOfWork.GetRepository<Employee, int>().GetAsyncWithSpec(spec);
            var employeeToReturn = mapper.Map<EmployeeToReturnDto>(employee);

            return employeeToReturn;
        }
    }
}
