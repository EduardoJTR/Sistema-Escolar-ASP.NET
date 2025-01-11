using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using System.Runtime.CompilerServices;

namespace SchoolSystem.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly DataBaseContext dataBaseContext;
        private static int? teacherId;

        public ProfessorController(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        private int GetTeacherFromUser()
        {
            int? userId = HttpContext.Session.GetInt32("Id");

            return dataBaseContext.teachers.FirstOrDefault(p => p.UserId == userId).Id;
        }

        public IActionResult Index()
        {
            try
            {
                if (teacherId == null)
                {
                    teacherId = GetTeacherFromUser();
                }
            }
            catch(Exception e)
            {
                return View("~/Views/Home/Index.cshtml");
            }


            var personalData = new
            {
                UserId = HttpContext.Session.GetInt32("Id"),
                Name = HttpContext.Session.GetString("Name"),
                Email = dataBaseContext.users.FirstOrDefault(u => u.Id ==
                            HttpContext.Session.GetInt32("Id")).Email,
                ClassName = (from std in dataBaseContext.students
                             join usr in dataBaseContext.users
                             on std.UserId equals usr.Id
                             join cls in dataBaseContext.classes
                             on std.ClassId equals cls.Id
                             where usr.Id == HttpContext.Session.GetInt32("Id")
                             select cls.ClassCode).FirstOrDefault()
            };

            return View(personalData);
        }

        public IActionResult Prova(int subjectId, int term, string classCode)
        {
            int classId = dataBaseContext.classes.FirstOrDefault(c => c.ClassCode == classCode).Id;

            if (!dataBaseContext.exams.Any(e => e.SubjectId == subjectId && e.Term == term && e.ClassId == classId))
            {
                Exam exam = new Exam
                {
                    SubjectId = subjectId,
                    Term = term,
                    ClassId = classId,
                    TeacherId = (int)teacherId
                };
                dataBaseContext.exams.Add(exam);
            }
            dataBaseContext.SaveChanges();

            int examId = dataBaseContext.exams.OrderByDescending(e => e.Id).LastOrDefault().Id;

            var data = new
            {
                classStudents = (from std in dataBaseContext.students
                                 where std.ClassId == classId
                                 select new
                                 {
                                     Name = std.Name,
                                     Id = std.Id
                                 }).ToList(),
                classCode = classCode,
                examId = examId
            };

            return View(data);
        }

        public IActionResult SaveExam(List<int> studentIds, List<float> studentGrades, string classCode, int examId)
        {
            for(int i = 0; i < studentIds.Count; i++)
            {
                dataBaseContext.examsGrades.Add( new ExamsGrade
                {
                    StudentId = studentIds[i],
                    Grade = (decimal)studentGrades[i],
                    ExamId = examId
                } );
            }

            dataBaseContext.SaveChanges();

            return RedirectToAction("Turma", "Professor", new { className = classCode });
        }

        public IActionResult MinhasAulas()
        {
            var Lessons = (from lss in dataBaseContext.lessons
                           where lss.TeacherId == teacherId
                           join sbj in dataBaseContext.subjects
                           on lss.SubjectId equals sbj.Id
                           join clss in dataBaseContext.classes
                           on lss.ClassId equals clss.Id
                           join clsrm in dataBaseContext.classRooms
                           on lss.ClassRoomId equals clsrm.Id
                           select new
                           {
                               StartTime = lss.StartTime,
                               EndTime = lss.EndTime,
                               WeekDay = lss.WeekDay,
                               RoomCode = clsrm.RoomCode,
                               ClassName = clss.ClassCode,
                               SubjectName = sbj.Name
                           }).ToList();

            return View(Lessons);
        }

        public IActionResult MinhasTurmas()
        {
            List<int> classIds = new List<int>();
            List<string> classNames = new List<string>();

            foreach(var lesson in dataBaseContext.lessons)
            {
                if (lesson.TeacherId == teacherId)
                {
                    if (!classIds.Contains(lesson.ClassId))
                    {
                        classIds.Add(lesson.ClassId);
                        classNames.Add(dataBaseContext.classes.FirstOrDefault(c => c.Id == lesson.ClassId).ClassCode);
                    }
                }
            }

            return View(classNames);
        }

        [HttpGet]
        public IActionResult Turma(string className)
        {
            var data = new
            {
                subjects = (from sbj in dataBaseContext.subjects
                            select new
                            {
                                Id = sbj.Id,
                                Name = sbj.Name
                            }).ToList(),

                ClassName = className,
                Works = (from wrks in dataBaseContext.works
                         select new
                         {
                             Title = wrks.Title,
                             Active = wrks.Active,
                             Id = wrks.Id
                         }).ToList().OrderByDescending(w => w.Id)
            };

            return View(data);
        }

        [HttpGet]
        public IActionResult Trabalho(int WorkId, string ClassCode)
        {
            int classId = dataBaseContext.classes.FirstOrDefault(c => c.ClassCode == ClassCode).Id;

            var data = new
            {
                classCode = ClassCode,
                workId = WorkId,
                description = dataBaseContext.works.FirstOrDefault(w => w.Id == WorkId).Description,
                title = dataBaseContext.works.FirstOrDefault(w => w.Id == WorkId).Title,
                maxGrade = dataBaseContext.works.FirstOrDefault(w => w.Id == WorkId).MaximumPoints,
                studentWork = (from cls in dataBaseContext.classes
                               where cls.Id == classId
                               join _std in dataBaseContext.students
                               on cls.Id equals _std.ClassId
                               select new
                               {
                                   Id = _std.Id,
                                   Name = _std.Name,
                                   Grade = dataBaseContext.worksGrades.FirstOrDefault(ws => ws.StudentId == _std.Id && ws.WorkId == WorkId).Grade,
                                   CompletedWork = dataBaseContext.worksGrades.Any(ws => ws.StudentId == _std.Id && ws.WorkId == WorkId)
                               }).ToList()
            };

            return View(data);
        }

        [HttpGet]
        public IActionResult TrabalhoCompleto(List<int> compleated, int workId, string classCode, List<float>grades, string ended)
        {
            int counter = 0;
            foreach(int id in compleated)
            {
                WorksGrade ws = new WorksGrade { StudentId = id, WorkId = workId, Grade = (decimal)grades[counter] };
                
                if (!dataBaseContext.worksGrades.Any(ws => ws.StudentId == id && ws.WorkId == workId))
                    dataBaseContext.worksGrades.Add(ws);
                counter++;
            }

            if (ended == "on")
            {
                dataBaseContext.works.FirstOrDefault(w => w.Id == workId).Active = false;
            }

            dataBaseContext.SaveChanges();

            return RedirectToAction("Turma", "Professor", new {className = classCode});
        }

        [HttpGet]
        public IActionResult AddTrabalho(string ClassName)
        {
            var data = new
            {
                name = ClassName,
                SubjectNames = dataBaseContext.subjects.Select(s => new { s.Name, s.Id }).ToList()
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult AddTrabalho(string title, string description, int maxPoints, int subjectId, string bin, string className)
        {
            int classId = dataBaseContext.classes.FirstOrDefault(c => c.ClassCode == className).Id;

            Work work = new Work
            {
                Title = title,
                TeacherId = (int)teacherId,
                Description = description,
                MaximumPoints = maxPoints,
                SubjectId = subjectId,
                Term = int.Parse(bin),
                ClassId = classId,
                Active = true
            };

            dataBaseContext.works.Add(work);
            dataBaseContext.SaveChanges();

            return RedirectToAction("Turma", "Professor", new {className = className});
        }
    }
}
