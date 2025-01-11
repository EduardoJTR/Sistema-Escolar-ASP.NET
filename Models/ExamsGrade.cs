using Azure.Identity;

namespace SchoolSystem.Models
{
    public class ExamsGrade
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public decimal? Grade { get; set; }
    }
}
