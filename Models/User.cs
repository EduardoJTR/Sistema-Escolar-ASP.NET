namespace SchoolSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PassWordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
