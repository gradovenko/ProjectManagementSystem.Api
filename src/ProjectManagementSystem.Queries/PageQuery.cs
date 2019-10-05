using MediatR;

namespace ProjectManagementSystem.Queries
{
    public abstract class PageQuery<T> : IRequest<Page<T>> where T : class
    {
        protected PageQuery(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }

        public int Limit { get; }

        public int Offset { get; }
    }
}