using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_webapi_demo_01_christenzarif.Repositories.Implement
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DataContext _dataContext;

        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Employee> Create(Employee employee)
        {
            if (employee is not null)
            {
                var emp = await _dataContext.AddAsync<Employee>(employee);
                await _dataContext.SaveChangesAsync();
                return employee;
            }
            return null!;
        }

        public async Task<bool> Delete(Guid guid)
        {
            Employee? employee = await _dataContext.Employees.FindAsync(guid);
            if (employee is not null)
            {
                _dataContext.Employees.Remove(employee);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Employee>> Get()
        {
            List<Employee> employees = await _dataContext.Employees.ToListAsync();
            return employees is not null ? employees : null!;
        }

        public async Task<Employee> Get(Guid guid)
        {
            Employee? employee = await _dataContext.Employees.FindAsync(guid);
            return employee is not null ? employee : null!;

        }

        public async Task<Employee> Get(string username)
        {
            Employee? employee = await _dataContext.Employees.FirstOrDefaultAsync(opt => opt.UserName == username);
            return employee is not null ? employee : null!;
        }

        public async Task<Employee> Update(Employee employee)
        {
            Employee? emp = await _dataContext.Employees.FindAsync(employee.Id);
            if (emp is not null)
            {
                emp.Name = employee.Name;
                emp.Address = employee.Address;
                emp.Email = employee.Email;
                emp.Salary = employee.Salary;
                emp.Age = employee.Age;
                emp.UpdatedAt = DateTime.Now;
                _dataContext.Employees.Update(emp);
                await _dataContext.SaveChangesAsync();
                return emp;
            }

            return null!;
        }
    }
}
