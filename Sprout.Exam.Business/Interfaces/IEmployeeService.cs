using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> Add(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto> Get(int id);
        Task<EmployeeDto> Update(EditEmployeeDto editEmployeeDto);
        Task<EmployeeDto> Delete(int id);
        Task<decimal> CalculateSalary(CalculateSalaryDto calculateSalaryDto);
    }
}
