using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SproutExamDbContext _db;
        public EmployeeRepository(SproutExamDbContext dbContext)
        {
            this._db = dbContext;
        }
        public async Task<Employee> Add(Employee employee)
        {
            try
            {
                await this._db.Employee.AddAsync(employee);
                await this._db.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> Delete(int id)
        {
            try
            {
                var employee = await this._db.Employee.FindAsync(id);

                if (employee != null)
                {
                    employee.IsDeleted = true;
                    await this._db.SaveChangesAsync();
                }
                else
                    return null;

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Employee> Get(int id)
        {
            try
            {
                return await this._db.Employee.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                return await this._db.Employee.Where(e => e.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> Update(Employee employee)
        {
            try
            {
                var existingEmployee = await this._db.Employee.FindAsync(employee.Id);

                if (existingEmployee != null)
                {
                    existingEmployee.FullName = employee.FullName;
                    existingEmployee.Birthdate = employee.Birthdate;
                    existingEmployee.Tin = employee.Tin;
                    existingEmployee.EmployeeTypeId = employee.EmployeeTypeId;

                    await this._db.SaveChangesAsync();
                }
                else
                    return null;

                return existingEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
