
namespace EmployeePostTrace.DataLayer;

public class StoredProcedures
{
    public const string Employee_Add = "Employee_Add";
    public const string Employee_Delete = "Employee_Delete";
    public const string Employee_GetById = "Employee_GetById";
    public const string Employee_GetAll = "Employee_GetAll";
    public const string Employee_Update = "Employee_Update";
    public const string Employee_GetByEmail = "Employee_GetByEmail";

    public const string Letter_Add = "Letter_Add";
    public const string Letter_Delete = "Letter_Delete";
    public const string Letter_GetAllByRecipientId = "Letter_GetAllByRecipientId";
    public const string Letter_GetAllBySenderId = "Letter_GetAllBySenderId";
    public const string Letter_GetById = "Letter_GetById";
    public const string Letter_Update = "Letter_Update";
}
