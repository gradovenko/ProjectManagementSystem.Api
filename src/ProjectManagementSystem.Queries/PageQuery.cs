using MediatR;

namespace ProjectManagementSystem.Queries;

public abstract record PageQuery<T>(int Offset, int Limit) : IRequest<PageViewModel<T>> where T : class;