using AutoMapper;
using EmployeePostTrace.Api.Models.Requests;
using EmployeePostTrace.Api.Models.Responses;
using EmployeePostTrace.BusinessLayer.Services.Interfaces;
using EmployeePostTrace.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePostTrace.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LetterController : ControllerBase
{
    private readonly ILetterService _letterService;
    private readonly IMapper _mapper;

    public LetterController (ILetterService letterService, IMapper mapper)
    {
        _letterService = letterService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> Add([FromBody] AddLetterRequest request)
    {
        var result = await _letterService.Add(_mapper.Map<LetterDto>(request));
        return Created("", result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Delete(int id)
    {
        await _letterService.Delete(id, true);
        return NoContent();
    }

    [Authorize]
    [HttpGet("/employee/{recipientId}/letters/recipient")]
    [ProducesResponseType(typeof(LetterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<LetterMainInfoResponse>>> GetAllByRecipientId(int recipientId)
    {
        var result = await _letterService.GetAllByRecipientId(recipientId);
        return Ok(_mapper.Map<List<LetterMainInfoResponse>>(result));
    }

    [Authorize]
    [HttpGet("/employee/{senderId}/letters/sender")]
    [ProducesResponseType(typeof(LetterMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<LetterMainInfoResponse>>> GetAllBySenderId(int senderId)
    {
        var result = await _letterService.GetAllBySenderId(senderId);
        return Ok(_mapper.Map<List<LetterMainInfoResponse>>(result));
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LetterAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterAllInfoResponse>> GetById(int id)
    {
        var result = await _letterService.GetById(id);
        if (result == null)
            return NotFound();
        else
            return Ok(_mapper.Map<LetterAllInfoResponse>(result));
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateLetter([FromBody] UpdateLetterRequest request, int id)
    {
        await _letterService.Update(_mapper.Map<LetterDto>(request), id);
        return NoContent();
    }
}
