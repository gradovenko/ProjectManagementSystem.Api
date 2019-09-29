namespace ProjectManagementSystem.WebApi.Models
{
    public class QueryPageBindModel
    {
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 10;
    }
}