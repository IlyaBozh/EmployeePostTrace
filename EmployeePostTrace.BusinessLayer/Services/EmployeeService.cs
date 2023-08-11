
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;

namespace EmployeePostTrace.BusinessLayer.Services;

public class EmployeeService : IEmployeeService 
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService (IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<int> Add(EmployeeDto employee)
    {
        bool inUniqueEmail = await CheckEmailForUniqueness(employee.Email);

       /* if (inUniqueEmail) { }*/

        /*employee.Password*/

        var employeeId = await _employeeRepository.Add(employee);

        return employeeId;
    }

    public async Task Delete(int id, bool isDeleted) => await _employeeRepository.Delete(id, isDeleted);

    public async Task<List<EmployeeDto>> GetAll() => await _employeeRepository.GetAll();

    public async Task<EmployeeDto> GetByEmail(string email)
    {
        var employee = await _employeeRepository.GetByEmail(email);

        /*if (employee == null) { }*/

        return employee;
    }

    public async Task<EmployeeDto> GetById(int id)
    {
        var employee = await _employeeRepository.GetById(id);

        /*if (employee == null) { }*/

        return employee;
    }

    public async Task Update(EmployeeDto NewEmployee, int id)
    {
        var employee = await _employeeRepository.GetById(id);

        employee.Id = id;
        employee.FirstName = NewEmployee.FirstName;
        employee.LastName = NewEmployee.LastName;
        employee.Patronymic = NewEmployee.Patronymic;

        await _employeeRepository.Update(employee);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _employeeRepository.GetByEmail(email) == default;
}
