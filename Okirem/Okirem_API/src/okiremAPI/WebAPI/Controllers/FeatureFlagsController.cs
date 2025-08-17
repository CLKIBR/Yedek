using Application.Features.FeatureFlags.Commands.Create;
using Application.Features.FeatureFlags.Commands.Delete;
using Application.Features.FeatureFlags.Commands.Update;
using Application.Features.FeatureFlags.Queries.GetById;
using Application.Features.FeatureFlags.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeatureFlagsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedFeatureFlagResponse>> Add([FromBody] CreateFeatureFlagCommand command)
    {
        CreatedFeatureFlagResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedFeatureFlagResponse>> Update([FromBody] UpdateFeatureFlagCommand command)
    {
        UpdatedFeatureFlagResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedFeatureFlagResponse>> Delete([FromRoute] Guid id)
    {
        DeleteFeatureFlagCommand command = new() { Id = id };

        DeletedFeatureFlagResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdFeatureFlagResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdFeatureFlagQuery query = new() { Id = id };

        GetByIdFeatureFlagResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListFeatureFlagListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFeatureFlagQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListFeatureFlagListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}