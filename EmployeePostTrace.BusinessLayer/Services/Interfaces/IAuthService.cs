
using EmployeePostTrace.BusinessLayer.Models;

namespace EmployeePostTrace.BusinessLayer.Services.Interfaces;

public interface IAuthService
{
    Task<ClaimModel> Login(string username, string password);
    public string GetToken (ClaimModel user);
}
