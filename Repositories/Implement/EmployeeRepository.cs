using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
            if (employee is null)
            {
                return null!;
            }
            var emp = await _dataContext.AddAsync<Employee>(employee);
            await _dataContext.SaveChangesAsync();
            return emp is not null ? employee : null!;
        }

        public Task<Employee> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> Get()
        {
            var employees = await _dataContext.Employees.ToListAsync();
            return employees is not null ? employees : null!;
        }

        public async Task<Employee> Get(Guid guid)
        {
            var employee = await _dataContext.Employees.FindAsync(guid);
            return employee is not null ? employee : null!;

        }

        public Task<Employee> Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
