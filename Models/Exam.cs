namespace SchoolSystem.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int Term { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
    }
}
