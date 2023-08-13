
using EmployeePostTrace.DataLayer.Models;

namespace EmployeePostTrace.BusinessLayer.Tests.TestModels;

public static class EmployeeDataForTest
{
    public static EmployeeDto GetEmployee()
    {
        var employee = new EmployeeDto() 
        { 
            FirstName = "Андрей",
            LastName = "Чернюк",
            Patronymic = "Александрович",
            Email = "andch@mail.ru",
            Password = "password",
            IsDeleted = false
        };

        return employee;
    }
}
