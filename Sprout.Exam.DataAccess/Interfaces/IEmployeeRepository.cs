using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public  interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<Employee> Delete(int id);
    }
}
