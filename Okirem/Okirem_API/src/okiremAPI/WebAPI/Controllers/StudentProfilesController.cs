using Application.Features.StudentProfiles.Commands.Create;
using Application.Features.StudentProfiles.Commands.Delete;
using Application.Features.StudentProfiles.Commands.Update;
using Application.Features.StudentProfiles.Queries.GetById;
using Application.Features.StudentProfiles.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentProfilesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedStudentProfileResponse>> Add([FromBody] CreateStudentProfileCommand command)
    {
        CreatedStudentProfileResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedStudentProfileResponse>> Update([FromBody] UpdateStudentProfileCommand command)
    {
        UpdatedStudentProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedStudentProfileResponse>> Delete([FromRoute] Guid id)
    {
        DeleteStudentProfileCommand command = new() { Id = id };

        DeletedStudentProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdStudentProfileResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdStudentProfileQuery query = new() { Id = id };

        GetByIdStudentProfileResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListStudentProfileListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentProfileQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListStudentProfileListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}