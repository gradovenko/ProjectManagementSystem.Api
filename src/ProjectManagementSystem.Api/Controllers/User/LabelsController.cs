using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Api.Models.User.Labels;
using ProjectManagementSystem.Domain.Labels.Commands;
using ProjectManagementSystem.Queries.Labels;

namespace ProjectManagementSystem.Api.Controllers.User;

[Authorize]
[ApiController]
[ProducesResponseType(401)]
public sealed class LabelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LabelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Create the label
    /// </summary>
    /// <param name="model">Input model</param>
    /// <response code="201">Label created</response>
    /// <response code="400">Validation failed</response>
    /// <response code="409">Label with same title already exists</response>
    [HttpPost("/labels")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Create(
        CancellationToken cancellationToken,
        [FromBody] CreateProjectLabelBindingModel model)
    {
        CreateLabelCommandResultState commandResultState = await _mediator.Send(new CreateLabelCommand
        {
            LabelId = model.Id,
            Title = model.Title,
            Description = model.Description,
            BackgroundColor = model.BackgroundColor
        }, cancellationToken);

        return commandResultState switch
        {
            CreateLabelCommandResultState.LabelWithSameTitleAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameTitleAlreadyExists.Title, ErrorCode.LabelWithSameTitleAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateLabelCommandResultState.LabelWithSameIdButOtherParamsAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameIdButOtherParamsAlreadyExists.Title, ErrorCode.LabelWithSameIdButOtherParamsAlreadyExists.Detail,
                HttpContext.Request.Path),
            CreateLabelCommandResultState.LabelCreated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Get the list of labels
    /// </summary>
    /// <response code="200">The list of labels</response>
    [HttpGet("/labels", Name = "GetLabelList")]
    [ProducesResponseType(typeof(IEnumerable<LabelListItemViewModel>), 200)]
    public async Task<IActionResult> GetList(
        CancellationToken cancellationToken,
        [FromQuery] GetLabelListBindingModel model)
    {
        return Ok(await _mediator.Send(new LabelListQuery(model.Offset, model.Limit), cancellationToken));
    }

    /// <summary>
    /// Update the label
    /// </summary>
    /// <param name="labelId">Label identifier</param>
    /// <param name="model">Input model</param>
    /// <response code="204">Label updated</response>
    /// <response code="400">Input model validation failed</response>
    /// <response code="404">Label not found</response>
    /// <response code="409">Label with same title already exists</response>
    [HttpPut("/labels/{labelId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 409)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Update(
        CancellationToken cancellationToken,
        [FromRoute] Guid labelId,
        [FromBody] UpdateLabelBindingModel model)
    {
        UpdateLabelCommandResultState commandResultState = await _mediator.Send(new UpdateLabelCommand
        {
            LabelId = labelId,
            Title = model.Title,
            Description = model.Description,
            BackgroundColor = model.BackgroundColor
        }, cancellationToken);

        return commandResultState switch
        {
            UpdateLabelCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            UpdateLabelCommandResultState.LabelWithSameTitleAlreadyExists => this.StatusCode(HttpStatusCode.Conflict, ErrorCode.LabelWithSameTitleAlreadyExists.Title, ErrorCode.LabelWithSameTitleAlreadyExists.Detail,
                HttpContext.Request.Path),
            UpdateLabelCommandResultState.LabelUpdated => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Delete the label
    /// </summary>
    /// <param name="labelId">Label identifier</param>
    /// <response code="204">Label deleted</response>
    /// <response code="404">Label not found</response>
    [HttpDelete("/labels/{labelId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 422)]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken,
        [FromRoute] Guid labelId)
    {
        DeleteLabelCommandResultState commandResultState = await _mediator.Send(new DeleteLabelCommand
        {
            LabelId = labelId
        }, cancellationToken);

        return commandResultState switch
        {
            DeleteLabelCommandResultState.LabelNotFound => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.LabelNotFound.Title, ErrorCode.LabelNotFound.Detail,
                HttpContext.Request.Path),
            DeleteLabelCommandResultState.LabelDeleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}