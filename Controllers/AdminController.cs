using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;

namespace SchoolSystem.Controllers
{
    public class AdminController : Controller
    {
        private DataBaseContext dataBaseContext;
        
        public AdminController(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Aulas()
        {
            var s = new
            {
                Classes = dataBaseContext.classes.ToList(),
                Subjects = dataBaseContext.subjects.ToList(),
                Teachers = dataBaseContext.teachers.ToList(),
                ClassRooms = dataBaseContext.classRooms.ToList(),
                LessonsTeacherSubject = from lss in dataBaseContext.lessons
                                        join tch in dataBaseContext.teachers
                                        on lss.TeacherId equals tch.Id
                                        join sbj in dataBaseContext.subjects
                                        on lss.SubjectId equals sbj.Id
                                        orderby lss.StartTime, lss.Id
                                        select new
                                        {
                                            StartTime = lss.StartTime,
                                            EndTime = lss.EndTime,
                                            WeekDay = lss.WeekDay,
                                            ClassId = lss.ClassId,
                                            TeacherName = tch.Name,
                                            SubjectName = sbj.Name
                                        }
            };
            return View(s);
        }

        public IActionResult AddAula(int classId, int subjectId, int teacherId, int classRoomId, string weekDay, TimeOnly startTime, TimeOnly endTime)
        {
            Lesson lesson = new Lesson
            {
                ClassId = classId,
                SubjectId = subjectId,
                TeacherId = teacherId,
                WeekDay = weekDay,
                ClassRoomId = classRoomId,
                StartTime = startTime,
                EndTime = endTime
            };

            dataBaseContext.lessons.Add(lesson);
            
            dataBaseContext.SaveChanges();

            return RedirectToAction("Aulas");
        }

        public IActionResult Salas()
        {
            return View(dataBaseContext.classRooms.ToList());
        }

        // In role, should receive 's' as Student, or 't' as teacher
        private int AddUser(string email, string name, string password, string role)
        {
            dataBaseContext.users.Add(new User
            {
                Email = email,
                Name = name,
                PassWordHash = password,
                Role = role
            });

            dataBaseContext.SaveChanges();

            return dataBaseContext.users.OrderBy(u => u.Id).Last().Id;
        }

        public IActionResult Professores()
        {
            return View(dataBaseContext.teachers.ToList());
        }

        public IActionResult AddTeacher(string name, string skills, string password, string email)
        {
            int userId = AddUser(email, name, password, "t");

            dataBaseContext.teachers.Add(new Teacher
            {
                UserId = userId,
                Name = name,
                Skills = skills
            });

            dataBaseContext.SaveChanges();

            return RedirectToAction("Professores");
        }

        public IActionResult Students()
        {
            var studentsClassCodes = new
            {
                students = (from s in dataBaseContext.students
                            join c in dataBaseContext.classes
                            on s.ClassId equals c.Id
                            select new
                            {
                                Id = s.Id,
                                UserId = s.UserId,
                                ClassCode = c.ClassCode,
                                Name = s.Name
                            }).ToList(),

                classCodes = (from c in dataBaseContext.classes
                              select new 
                              { 
                                  code = c.ClassCode,
                                  id = c.Id
                              }).ToList()
            };

            return View("~/Views/Admin/Estudantes.cshtml", studentsClassCodes);
        }

        // IMPORTANT, AddStudent also adds a new user
        [HttpGet]
        public IActionResult AddStudent(string classId, string name, string password, string email)
        {
            int userId = AddUser(email, name, password, "s");

            dataBaseContext.students.Add(new Student
            {
                Name = name,
                ClassId = int.Parse(classId),
                UserId = userId
            });

            dataBaseContext.SaveChanges();

            return RedirectToAction("Students");
        }

        public IActionResult Classes()
        {
            return View("~/Views/Admin/Classes.cshtml", dataBaseContext.classes.ToList());
        }

        [HttpGet]
        public IActionResult AddClass(string grade, string name)
        {
            Class _class = new Class { Grade = int.Parse(grade), ClassCode = name };

            dataBaseContext.classes.Add(_class);
            dataBaseContext.SaveChanges();
            return View("~/Views/Admin/Classes.cshtml", dataBaseContext.classes.ToList());
        }
    }
}
