
using Dapper;
using EmployeePostTrace.DataLayer.Models;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using System.Data;

namespace EmployeePostTrace.DataLayer.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(IDbConnection connection) : base(connection) { }


    public async Task<int> Add(EmployeeDto employeeDto)
    {
        var id = await _connectionString.QuerySingleAsync<int>(
            StoredProcedures.Employee_Add,
            param: new
            {
                employeeDto.FirstName,
                employeeDto.LastName,
                employeeDto.Patronymic,
                employeeDto.Email,
                employeeDto.Password,
            },
            commandType: CommandType.StoredProcedure);

        return id;
    }

    public async Task Delete(int id, bool isDeleted)
    {
        await _connectionString.ExecuteAsync(
            StoredProcedures.Employee_Delete,
            param: new { id, isDeleted },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<List<EmployeeDto>> GetAll()
    {
        var leads = (await _connectionString.QueryAsync<EmployeeDto>(
            StoredProcedures.Employee_GetAll,
            commandType: CommandType.StoredProcedure))
            .ToList();

        return leads;
    }

    public async Task<EmployeeDto> GetByEmail(string email)
    {
        var lead = await _connectionString.QueryFirstOrDefaultAsync<EmployeeDto>(
            StoredProcedures.Employee_GetByEmail,
            param: new { email },
            commandType: CommandType.StoredProcedure);

        return lead;
    }

    public async Task<EmployeeDto> GetById(int id)
    {
        var employee = (await _connectionString.QueryAsync<EmployeeDto>(
            StoredProcedures.Employee_GetById,
            param: new { id },
            commandType: CommandType.StoredProcedure)).FirstOrDefault();

        return employee;
    }

    public async Task Update(EmployeeDto employeeDto)
    {
        await _connectionString.ExecuteAsync(
            StoredProcedures.Employee_Update,
            param: new
            {
                employeeDto.Id,
                employeeDto.FirstName,
                employeeDto.LastName,
                employeeDto.Patronymic,
            },
            commandType: CommandType.StoredProcedure);
    }
}
