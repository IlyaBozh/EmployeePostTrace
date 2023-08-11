using AutoMapper;
using EmployeePostTrace.Api.Models.Requests;
using EmployeePostTrace.Api.Models.Responses;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeePostTrace.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> Register([FromBody] RegistrationEmployeeRequest request)
    {
        var result = await _employeeService.Add(_mapper.Map<EmployeeDto>(request));

        return Created($"{this.GetUrl()}/{result}", result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Remove(int id)
    {
        var claims = this.GetClaims();
        var lead = await _employeeService.GetById(id);
        await _employeeService.Delete(id, true);
        return NoContent();
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(List<EmployeeMainInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<EmployeeMainInfoResponse>>> GetAll()
    {
        var leads = await _employeeService.GetAll();
        return Ok(_mapper.Map<List<EmployeeMainInfoResponse>>(leads));
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeAllInfoResponse>> GetById(int id)
    {
        var claims = this.GetClaims();
        var lead = await _employeeService.GetById(id, claims);

        if (lead is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(_mapper.Map<EmployeeAllInfoResponse>(lead));
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Update([FromBody] UpdateEmployeeRequest request, int id)
    {
        await _employeeService.Update(_mapper.Map<EmployeeDto>(request), id);
        return NoContent();
    }
}
