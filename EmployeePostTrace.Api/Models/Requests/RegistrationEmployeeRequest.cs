namespace EmployeePostTrace.Api.Models.Requests;

public class RegistrationEmployeeRequest : UpdateEmployeeRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
