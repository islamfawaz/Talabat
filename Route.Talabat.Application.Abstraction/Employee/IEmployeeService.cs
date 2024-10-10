using Route.Talabat.Application.Abstraction.Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Application.Abstraction.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync();
        Task<EmployeeToReturnDto> GetEmployeeByIdAsync(int id);
    }
}
