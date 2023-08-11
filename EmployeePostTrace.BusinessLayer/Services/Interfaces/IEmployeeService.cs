
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.BusinessLayer.Services.Interfaces;

public interface IEmployeeService
{
    Task<int> Add (EmployeeDto employee);
    Task Delete (int id, bool isDeleted);
    Task<List<EmployeeDto>> GetAll ();
    Task<EmployeeDto> GetByEmail (string email);
    Task<EmployeeDto> GetById (int id);
    Task Update (EmployeeDto NewEmployee, int id);
}
