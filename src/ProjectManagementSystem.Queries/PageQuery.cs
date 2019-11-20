using MediatR;

namespace ProjectManagementSystem.Queries
{
    public abstract class PageQuery<T> : IRequest<Page<T>> where T : class
    {
        protected PageQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
        
        public int Offset { get; }
        
        public int Limit { get; }
    }
}