using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePriorityListQueryHandler : IRequestHandler<IssuePriorityListQuery, Page<IssuePriorityListItemView>>
    {
        private readonly IDbConnection _dbConnection;

        public IssuePriorityListQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Page<IssuePriorityListItemView>> Handle(IssuePriorityListQuery query,
            CancellationToken cancellationToken)
        {
            var sqlCount = _dbConnection.QueryAsync<int>(@"
SELECT COUNT(*)
FROM ""IssuePriority""
");
            
            var sqlItems = _dbConnection.QueryAsync<IssuePriorityListItemView>($@"
SELECT i.""IssuePriorityId"" AS ""Id"", i.""Name"", i.""IsActive""
FROM ""IssuePriority"" AS i
ORDER BY (SELECT 1)
LIMIT {} OFFSET @__p_0
");
                
                
                
                .IssuePriorities.AsNoTracking()
                .Select(issuePriority => new IssuePriorityListItemView
                {
                    Id = issuePriority.Id,
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                });

            return new Page<IssuePriorityListItemView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}