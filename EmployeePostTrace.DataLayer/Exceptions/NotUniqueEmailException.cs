
namespace EmployeePostTrace.DataLayer.Exceptions;

public class NotUniqueEmailException : Exception
{
    public NotUniqueEmailException(string message) : base(message) 
    {
    
    }
}
