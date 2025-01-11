namespace SchoolSystem.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string WeekDay { get; set; } // Seg Ter Qua Qui Sex
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int ClassRoomId { get; set; }
        public int SubjectId { get; set; }
    }
}
