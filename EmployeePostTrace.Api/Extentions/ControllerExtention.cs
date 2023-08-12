using EmployeePostTrace.BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeePostTrace.Api.Extentions;

public static class ControllerExtention
{
    public static string GetUrl(this ControllerBase controller) =>
        $"{controller.Request?.Scheme}://{controller.Request?.Host.Value}{controller.Request?.Path.Value}";

    public static ClaimModel GetClaims(this ControllerBase controller)
    {
        ClaimModel claimModel = new();
        if (controller.User is not null)
        {
            var claims = controller.User.Claims.ToList();
            claimModel.Id = Int32.Parse(claims[0].Value);
        }

        return claimModel;
    }
}
