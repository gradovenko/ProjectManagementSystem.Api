// using System.Net;
// using MediatR;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using ProjectManagementSystem.Api.Exceptions;
// using ProjectManagementSystem.Api.Extensions;
// using ProjectManagementSystem.Api.Models.Admin.Users;
// using ProjectManagementSystem.Domain.Users.Commands;
// using ProjectManagementSystem.Queries;
// using ProjectManagementSystem.Queries.Admin.Users;
// using UserRole = ProjectManagementSystem.Domain.Users.UserRole;
//
// namespace ProjectManagementSystem.Api.Controllers.Admin;
//
// [Authorize(Roles = "Admin")]
// [ApiController]
// [ProducesResponseType(401)]
// public sealed class UsersController : ControllerBase
// {
//     private readonly IMediator _mediator;
//
//     public UsersController(IMediator mediator)
//     {
//         _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
//     }
//
//     /// <summary>
//     /// Create the user
//     /// </summary>
//     /// <param name="model">Input model</param>
//     /// <response code="201">Created user</response>
//     /// <response code="400">Validation failed</response>
//     /// <response code="409">User / name / email already exists with other parameters</response>
//     [HttpPost("/admin/users")]
//     [ProducesResponseType(typeof(CreatedAtRouteResult), (int)HttpStatusCode.Created)]
//     [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
//     [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Conflict)]
//     [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
//     public async Task<IActionResult> Create(
//         CancellationToken cancellationToken, 
//         [FromBody] CreateUserBindingModel model)
//     {
//         CreateUserCommandResultState commandResult = await _mediator.Send(new CreateUserCommand
//         {
//             UserId = model.Id,
//             Name = model.Name,
//             Email = model.Email,
//             Password = model.Password,
//             FirstName = model.FirstName,
//             LastName = model.LastName,
//             Role = model.Role.ConvertTo<UserRole>()
//         }, cancellationToken);
//
//         return commandResult switch
//         {
//             CreateUserCommandResultState.UserWithSameIdButOtherParamsAlreadyExists => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
//                 HttpContext.Request.Path),
//             CreateUserCommandResultState.UserSameNameAlreadyExists => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
//                 HttpContext.Request.Path),
//             CreateUserCommandResultState.UserWithSameEmailAlreadyExists => this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
//                 HttpContext.Request.Path),
//             CreateUserCommandResultState.UserCreated => CreatedAtRoute(nameof(Get), new { id = model.Id }, null),
//             _ => throw new ArgumentOutOfRangeException()
//         };
//     }
//
//     /// <summary>
//     /// Get the list of users
//     /// </summary>
//     /// <param name="binding">Input model</param>
//     /// <response code="200">User list page</response>
//     [HttpGet("/admin/users", Name = "GetUserList")]
//     [ProducesResponseType(typeof(PageViewModel<UserListItemViewModel>), 200)]
//     public async Task<IActionResult> GetList(
//         CancellationToken cancellationToken,
//         [FromQuery] GetUserListBindingModel binding)
//     {
//         return Ok(await _mediator.Send(new UserListQuery(binding.Offset, binding.Limit), cancellationToken));
//     }
//         
//     /// <summary>
//     /// Get the user
//     /// </summary>
//     /// <param name="id">User identifier</param>
//     /// <response code="200">User</response>
//     /// <response code="404">User not found</response>
//     [HttpGet("/admin/users/{id:guid}", Name = "GetUser")]
//     [ProducesResponseType(typeof(UserView), 200)]
//     [ProducesResponseType(typeof(ProblemDetails), 404)]
//     public async Task<IActionResult> Get(
//         CancellationToken cancellationToken,
//         [FromRoute] Guid id)
//     {
//         UserView? user = await _mediator.Send(new UserQuery(id), cancellationToken);
//
//         if (user == null)
//             return this.StatusCode(HttpStatusCode.NotFound, ErrorCode.UserNotFound.Title, ErrorCode.UserNotFound.Detail,
//                 HttpContext.Request.Path);
//
//         return Ok(user);
//     }
// }
//