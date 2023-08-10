
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.DataLayer.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<int> Add(EmployeeDto employeeDto);
    Task Delete(int id, bool isDeleted);
    Task<EmployeeDto> GetById(int id);
    Task<EmployeeDto> Update(EmployeeDto employeeDto);
}
