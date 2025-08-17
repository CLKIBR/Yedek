using Application.Features.ParentStudentLinks.Commands.Create;
using Application.Features.ParentStudentLinks.Commands.Delete;
using Application.Features.ParentStudentLinks.Commands.Update;
using Application.Features.ParentStudentLinks.Queries.GetById;
using Application.Features.ParentStudentLinks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParentStudentLinksController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedParentStudentLinkResponse>> Add([FromBody] CreateParentStudentLinkCommand command)
    {
        CreatedParentStudentLinkResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedParentStudentLinkResponse>> Update([FromBody] UpdateParentStudentLinkCommand command)
    {
        UpdatedParentStudentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedParentStudentLinkResponse>> Delete([FromRoute] Guid id)
    {
        DeleteParentStudentLinkCommand command = new() { Id = id };

        DeletedParentStudentLinkResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdParentStudentLinkResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdParentStudentLinkQuery query = new() { Id = id };

        GetByIdParentStudentLinkResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListParentStudentLinkListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListParentStudentLinkQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListParentStudentLinkListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}