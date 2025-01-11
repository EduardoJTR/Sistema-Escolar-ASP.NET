using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorksGrade>().HasKey(ws => new { ws.StudentId, ws.WorkId });

            modelBuilder.Entity<ExamsGrade>().HasKey(es => new { es.StudentId, es.ExamId });
        }

        public DbSet<User> users { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<ClassRoom> classRooms { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Work> works { get; set; }
        public DbSet<WorksGrade> worksGrades { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<ExamsGrade> examsGrades { get; set; }
    }
}
