namespace ProjectManagementSystem.WebApi.Models.Admin.Users
{
    public class QueryUserBinding
    {
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 10;
    }
}