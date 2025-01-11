namespace SchoolSystem.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int Term { get; set; }
        public string Description { get; set; }
        public int MaximumPoints { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
    }
}
