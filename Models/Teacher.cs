namespace SchoolSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        // Equivalente as competências do professor
        public string Skills { get; set; }
    }
}
