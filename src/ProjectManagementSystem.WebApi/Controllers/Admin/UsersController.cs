using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain.Admin.CreateUsers;
using ProjectManagementSystem.Queries.Admin.Users;
using ProjectManagementSystem.WebApi.Exceptions;
using ProjectManagementSystem.WebApi.Models.Admin.Users;

namespace ProjectManagementSystem.WebApi.Controllers.Admin
{
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
                if (!user.UserName.Equals(binding.UserName) ||
                    !user.Email.Equals(binding.Email))
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCode.UserAlreadyExists, "User already exists with other parameters");

            user = await userRepository.FindByUserName(binding.UserName, cancellationToken);

            if (user != null)
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.UsernameAlreadyExists, "Username already exists");

            user = await userRepository.FindByEmail(binding.Email, cancellationToken);

            if (user != null)
                throw new ApiException(HttpStatusCode.Conflict, ErrorCode.EmailAlreadyExists, "Email already exists");

            var passwordHash = passwordHasher.HashPassword(binding.Password);

            user = new Domain.Admin.CreateUsers.User(binding.Id, binding.UserName, binding.Email, passwordHash, binding.FirstName, binding.LastName, Enum.Parse<Domain.Admin.CreateUsers.UserRole>(binding.Role.ToString()));

            await userRepository.Save(user);

            return CreatedAtRoute("GetUserAdminRoute", new { id = user.Id }, null);
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id">User ID</param>
        /// <param name="queryProcessor"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpGet("admin/users/{id}", Name = "GetUserAdminRoute")]
        [ProducesResponseType(typeof(ShortUserView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken,
            [FromRoute] Guid id,
            [FromServices] IQueryProcessor queryProcessor)
        {
            var user = await queryProcessor.ProcessAsync(new UserQuery(id), cancellationToken);

            if (user == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCode.UserNotFound, "User not found");

            return Ok(user);
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="binding"></param>
        /// <param name="queryProcessor"></param>
        /// <returns></returns>
        [HttpGet("admin/users", Name = "FindUsersAdminRoute")]
        [ProducesResponseType(typeof(ShortUserView), 200)]
        public async Task<IActionResult> Find(
            CancellationToken cancellationToken,
            [FromQuery] QueryUserBinding binding,
            [FromServices] IQueryProcessor queryProcessor)
        {
            return Ok(await queryProcessor.ProcessAsync(new UsersQuery(binding.Offset, binding.Limit), cancellationToken));
        }
    }
}