using Application.Features.AdminProfiles.Commands.Create;
using Application.Features.AdminProfiles.Commands.Delete;
using Application.Features.AdminProfiles.Commands.Update;
using Application.Features.AdminProfiles.Queries.GetById;
using Application.Features.AdminProfiles.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminProfilesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedAdminProfileResponse>> Add([FromBody] CreateAdminProfileCommand command)
    {
        CreatedAdminProfileResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedAdminProfileResponse>> Update([FromBody] UpdateAdminProfileCommand command)
    {
        UpdatedAdminProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedAdminProfileResponse>> Delete([FromRoute] Guid id)
    {
        DeleteAdminProfileCommand command = new() { Id = id };

        DeletedAdminProfileResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAdminProfileResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdAdminProfileQuery query = new() { Id = id };

        GetByIdAdminProfileResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListAdminProfileListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAdminProfileQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListAdminProfileListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}