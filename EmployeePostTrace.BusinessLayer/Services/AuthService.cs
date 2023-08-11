
using EmployeePostTrace.BusinessLayer.Exceptions;
using EmployeePostTrace.BusinessLayer.Infrastructure;
using EmployeePostTrace.BusinessLayer.Models;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeePostTrace.BusinessLayer.Services;

public class AuthService : IAuthService 
{
    private readonly IEmployeeRepository _employeeRepository;

    public AuthService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public string GetToken(ClaimModel user)
    {
        var claims = new List<Claim>
        {
            { new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()) }
        };

        var jwt = new JwtSecurityToken(
            issuer: TokenOptions.Issuer,
            audience: TokenOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<ClaimModel> Login(string username, string password)
    {
        ClaimModel claimModel = new ClaimModel();

        var employee = await _employeeRepository.GetByEmail(username);

        if (employee == null)
        {
            throw new NotFoundException("Сотрудник не найден");
        }

        if (PasswordHash.ValidatePassword(password, employee.Password))
        {
            claimModel.Id = employee.Id;
        }

        return claimModel;
    }
}
