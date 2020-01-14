using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.Exceptions;
using ProjectManagementSystem.Api.Models.Admin.Users;
using ProjectManagementSystem.Domain.Admin.Users;
using ProjectManagementSystem.Queries.Admin.Users;
using UserRole = ProjectManagementSystem.Domain.Admin.Users.UserRole;

namespace ProjectManagementSystem.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public sealed class UsersController : ControllerBase
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding">Input model</param>
        /// <param name="userRepository"></param>
        /// <param name="passwordHasher"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("admin/users")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        public async Task<IActionResult> CreateUser(
            CancellationToken cancellationToken, 
            [FromBody] CreateUserBinding binding,
            [FromServices] IUserRepository userRepository,
            [FromServices] IPasswordHasher passwordHasher)
        {
            var user = await userRepository.Get(binding.Id, cancellationToken);

            if (user != null)
                if (!user.Name.Equals(binding.Name) ||
                    !user.Email.Equals(binding.Email))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.UserAlreadyExists, "User already exists with other parameters");

            user = await userRepository.GetByName(binding.Name, cancellationToken);

            if (user != null)
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.UsernameAlreadyExists, "Username already exists");

            user = await userRepository.GetByEmail(binding.Email, cancellationToken);

            if (user != null)
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.EmailAlreadyExists, "Email already exists");

            var passwordHash = passwordHasher.HashPassword(binding.Password);

            user = new Domain.Admin.Users.User(binding.Id, binding.Name, binding.Email, passwordHash, binding.FirstName, binding.LastName, Enum.Parse<UserRole>(binding.Role.ToString()));

            await userRepository.Save(user);

            return CreatedAtRoute("GetUserAdminRoute", new { id = user.Id }, null);
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id">User ID</param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet("admin/users/{id}", Name = "GetUserAdminRoute")]
        [ProducesResponseType(typeof(ShortUserView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IMediator mediator)
        {
            var user = await mediator.Send(new UserQuery(id), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.UserNotFound, "User not found");

            return Ok(user);
        }

        /// <summary>
        /// Find users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet("admin/users", Name = "FindUsersAdminRoute")]
        [ProducesResponseType(typeof(ShortUserView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] FindUsersBinding binding,
            [FromServices] IMediator mediator)
        {
            return Ok(await mediator.Send(new UsersQuery(binding.Offset, binding.Limit), cancellationToken));
        }
    }
}