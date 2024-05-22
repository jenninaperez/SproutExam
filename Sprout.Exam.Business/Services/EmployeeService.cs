using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public async Task<EmployeeDto> Add(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var newEmployee = new Employee()
                {
                    FullName = createEmployeeDto.FullName,
                    Birthdate = createEmployeeDto.Birthdate,
                    Tin = createEmployeeDto.Tin,
                    EmployeeTypeId = createEmployeeDto.TypeId
                };

                newEmployee = await this._employeeRepository.Add(newEmployee);

                var employeeDto = new EmployeeDto()
                {
                    Id = newEmployee.Id,
                    FullName = newEmployee.FullName,
                    Birthdate = newEmployee.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = newEmployee.Tin,
                    TypeId = newEmployee.EmployeeTypeId
                };

                return employeeDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> CalculateSalary(CalculateSalaryDto calculateSalaryDto)
        {
            decimal salary = 0;
            try
            {
                var existingEmployee = await this.Get(calculateSalaryDto.EmployeeId);
                var employeeType = (EmployeeType)existingEmployee.TypeId;

                switch (employeeType)
                {
                    case EmployeeType.Regular:
                        decimal monthlyRate = 20000;
                        //Compute deduction for absences
                        decimal absences = calculateSalaryDto.AbsentDays * (monthlyRate / 22);
                        //Compute tax deduction
                        decimal tax = monthlyRate * 0.12m;
                        //Deduct absences and tax
                        salary = monthlyRate - absences - tax;
                        break;
                    case EmployeeType.Contractual:
                        decimal dailyRate = 500;
                        salary = dailyRate * calculateSalaryDto.WorkedDays;
                        break;
                    default:
                        break;
                }
                return salary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> Delete(int id)
        {
            try
            {
                var deletedEmployee = await this._employeeRepository.Delete(id);

                var employeeDto = new EmployeeDto()
                {
                    Id = deletedEmployee.Id,
                    FullName = deletedEmployee.FullName,
                    Birthdate = deletedEmployee.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = deletedEmployee.Tin,
                    TypeId = deletedEmployee.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            try
            {
                var existingEmployees = (await this._employeeRepository.GetAllEmployees())
                                                                        .Select(
                                                                            e => new EmployeeDto()
                                                                            {
                                                                                Id = e.Id,
                                                                                FullName = e.FullName,
                                                                                Birthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                                                                                Tin = e.Tin,
                                                                                TypeId = e.EmployeeTypeId
                                                                            }
                                                                        ).ToList();
                return existingEmployees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> Get(int id)
        {
            try
            {
                var employee = await this._employeeRepository.Get(id);

                var employeeDto = new EmployeeDto()
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = employee.Tin,
                    TypeId = employee.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeDto> Update(EditEmployeeDto editEmployeeDto)
        {
            try
            {
                var employeeToUpdate = new Employee()
                {
                    Id = editEmployeeDto.Id,
                    FullName = editEmployeeDto.FullName,
                    Birthdate = editEmployeeDto.Birthdate,
                    Tin = editEmployeeDto.Tin,
                    EmployeeTypeId = editEmployeeDto.TypeId
                };

                var updatedEmployee = await this._employeeRepository.Update(employeeToUpdate);

                var employeeDto = new EmployeeDto()
                {
                    Id = updatedEmployee.Id,
                    FullName = updatedEmployee.FullName,
                    Birthdate = updatedEmployee.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = updatedEmployee.Tin,
                    TypeId = updatedEmployee.EmployeeTypeId
                };

                return employeeDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
