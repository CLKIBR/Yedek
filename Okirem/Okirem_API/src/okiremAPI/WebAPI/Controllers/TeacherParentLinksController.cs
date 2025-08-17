using Application.Features.TeacherParentLinks.Commands.Create;
using Application.Features.TeacherParentLinks.Commands.Delete;
using Application.Features.TeacherParentLinks.Commands.Update;
using Application.Features.TeacherParentLinks.Queries.GetById;
using Application.Features.TeacherParentLinks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherParentLinksController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedTeacherParentLinkResponse>> Add([FromBody] CreateTeacherParentLinkCommand command)
    {
        CreatedTeacherParentLinkResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedTeacherParentLinkResponse>> Update([FromBody] UpdateTeacherParentLinkCommand command)
    {
        UpdatedTeacherParentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedTeacherParentLinkResponse>> Delete([FromRoute] Guid id)
    {
        DeleteTeacherParentLinkCommand command = new() { Id = id };

        DeletedTeacherParentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdTeacherParentLinkResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdTeacherParentLinkQuery query = new() { Id = id };

        GetByIdTeacherParentLinkResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListTeacherParentLinkListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeacherParentLinkQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListTeacherParentLinkListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}