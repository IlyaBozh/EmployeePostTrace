
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeePostTrace.BusinessLayer.Models;

public class TokenOptions
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    const string Key = "mysupersecret_secretkey!123456789090059609399655";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
