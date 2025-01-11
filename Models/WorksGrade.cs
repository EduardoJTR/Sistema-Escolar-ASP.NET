using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Models
{
    public class WorksGrade
    {
        public int StudentId { get; set; }
        public int WorkId { get; set; }
        public decimal? Grade { get; set; }
    }
}
