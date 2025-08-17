using Application.Features.ParentProfiles.Commands.Create;
using Application.Features.ParentProfiles.Commands.Delete;
using Application.Features.ParentProfiles.Commands.Update;
using Application.Features.ParentProfiles.Queries.GetById;
using Application.Features.ParentProfiles.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParentProfilesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedParentProfileResponse>> Add([FromBody] CreateParentProfileCommand command)
    {
        CreatedParentProfileResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedParentProfileResponse>> Update([FromBody] UpdateParentProfileCommand command)
    {
        UpdatedParentProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedParentProfileResponse>> Delete([FromRoute] Guid id)
    {
        DeleteParentProfileCommand command = new() { Id = id };

        DeletedParentProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdParentProfileResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdParentProfileQuery query = new() { Id = id };

        GetByIdParentProfileResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListParentProfileListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListParentProfileQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListParentProfileListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}