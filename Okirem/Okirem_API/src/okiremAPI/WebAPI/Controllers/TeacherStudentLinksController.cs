using Application.Features.TeacherStudentLinks.Commands.Create;
using Application.Features.TeacherStudentLinks.Commands.Delete;
using Application.Features.TeacherStudentLinks.Commands.Update;
using Application.Features.TeacherStudentLinks.Queries.GetById;
using Application.Features.TeacherStudentLinks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherStudentLinksController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedTeacherStudentLinkResponse>> Add([FromBody] CreateTeacherStudentLinkCommand command)
    {
        CreatedTeacherStudentLinkResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedTeacherStudentLinkResponse>> Update([FromBody] UpdateTeacherStudentLinkCommand command)
    {
        UpdatedTeacherStudentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedTeacherStudentLinkResponse>> Delete([FromRoute] Guid id)
    {
        DeleteTeacherStudentLinkCommand command = new() { Id = id };

        DeletedTeacherStudentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdTeacherStudentLinkResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdTeacherStudentLinkQuery query = new() { Id = id };

        GetByIdTeacherStudentLinkResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListTeacherStudentLinkListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTeacherStudentLinkQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListTeacherStudentLinkListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}