﻿using dotnet_webapi_demo_01_christenzarif.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_demo_01_christenzarif.Repositories.Interface
{
    public interface IEmployee
    {
        Task<List<Employee>> Get();
        Task<Employee> Get(Guid id);
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<Employee> Delete(Guid id);
    }
}
