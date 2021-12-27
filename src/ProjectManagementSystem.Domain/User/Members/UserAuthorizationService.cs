using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Domain.User.Members
{
    public class UserAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMemberRepository _memberRepository;

        public UserAuthorizationService(IUserRepository userRepository, IProjectRepository projectRepository,
            IPermissionRepository permissionRepository, IMemberRepository memberRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _permissionRepository = permissionRepository;
            _memberRepository = memberRepository;
        }

        public async Task<bool> Authorization(Guid userId, Guid projectId, string permissionName,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(userId, cancellationToken);

            if (user == null)
                throw new Exception();

            var project = await _projectRepository.Get(projectId, cancellationToken);

            if (project == null)
                throw new Exception();

            var permission = await _permissionRepository.Get(permissionName, cancellationToken);

            if (permission == null)
                throw new Exception();

            foreach (var rolePermission in permission.RolePermissions)
            {
                if (await _memberRepository.Get(userId, projectId, rolePermission.RoleId, cancellationToken) != null)
                    return true;
            }

            return false;
        }
    }
}