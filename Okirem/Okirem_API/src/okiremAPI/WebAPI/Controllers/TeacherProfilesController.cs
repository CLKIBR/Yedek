using Application.Features.TeacherProfiles.Commands.Create;
using Application.Features.TeacherProfiles.Commands.Delete;
using Application.Features.TeacherProfiles.Commands.Update;
using Application.Features.TeacherProfiles.Queries.GetById;
using Application.Features.TeacherProfiles.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherProfilesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedTeacherProfileResponse>> Add([FromBody] CreateTeacherProfileCommand command)
    {
        CreatedTeacherProfileResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedTeacherProfileResponse>> Update([FromBody] UpdateTeacherProfileCommand command)
    {
        UpdatedTeacherProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedTeacherProfileResponse>> Delete([FromRoute] Guid id)
    {
        DeleteTeacherProfileCommand command = new() { Id = id };

        DeletedTeacherProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdTeacherProfileResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdTeacherProfileQuery query = new() { Id = id };

        GetByIdTeacherProfileResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListTeacherProfileListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeacherProfileQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListTeacherProfileListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}