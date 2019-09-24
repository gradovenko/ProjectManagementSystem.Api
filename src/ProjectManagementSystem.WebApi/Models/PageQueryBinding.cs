namespace ProjectManagementSystem.WebApi.Models
{
    public class PageQueryBinding
    {
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 10;
    }
}