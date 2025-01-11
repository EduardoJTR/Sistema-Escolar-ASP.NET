using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using System.Diagnostics.Contracts;

namespace SchoolSystem.Controllers
{
    public class AlunoController : Controller
    {
        private DataBaseContext dataBaseContext;
        private static int? StudentId;
        private static int? ClassId;

        public AlunoController(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public IActionResult Horarios()
        {
            var horarios = from lss in dataBaseContext.lessons
                           join cls in dataBaseContext.classes
                           on lss.ClassId equals cls.Id
                           join sbj in dataBaseContext.subjects
                           on lss.SubjectId equals sbj.Id
                           join tchr in dataBaseContext.teachers
                           on lss.TeacherId equals tchr.Id
                           join clsr in dataBaseContext.classRooms
                           on lss.ClassRoomId equals clsr.Id
                           where cls.Id == ClassId
                           orderby lss.StartTime ascending
                           select new
                           {
                               StartTime = lss.StartTime,
                               EndTime = lss.EndTime,
                               WeekDay = lss.WeekDay,
                               SubjectName = sbj.Name,
                               TeacherName = tchr.Name,
                               ClassRoomName = clsr.RoomCode
                           };

            var data = new
            {
                _horarios = horarios,
                className = dataBaseContext.classes.FirstOrDefault(c => c.Id == ClassId)
            };

            return View(data);
        }

        public IActionResult Notas()
        {
            var WorksGrades = (from wrkg in dataBaseContext.worksGrades
                               join wrk in dataBaseContext.works
                               on wrkg.WorkId equals wrk.Id
                               join sbj in dataBaseContext.subjects
                               on wrk.SubjectId equals sbj.Id
                               where wrkg.StudentId == StudentId
                               select new
                               {
                                   term = wrk.Term,
                                   grade = wrkg.Grade,
                                   title = wrk.Title,
                                   subjectName = sbj.Name,
                                   subjectId = sbj.Id
                               }).ToList();

            var ExamGrades = (from exgr in dataBaseContext.examsGrades
                              join ex in dataBaseContext.exams
                              on exgr.ExamId equals ex.Id
                              join sbj in dataBaseContext.subjects
                              on ex.SubjectId equals sbj.Id
                              where exgr.StudentId == StudentId
                              select new
                              {
                                  term = ex.Term,
                                  grade = exgr.Grade,
                                  subjectName = sbj.Name,
                                  subjectId = sbj.Id
                              }).ToList();


            var term1Work = from wgr in WorksGrades
                            where wgr.term == 1
                            select wgr;
            var term1Exam = (from egr in ExamGrades
                             where egr.term == 1
                             select egr).FirstOrDefault();


            var term2Work = from wgr in WorksGrades
                            where wgr.term == 2
                            select wgr;
            var term2Exam = (from egr in ExamGrades
                             where egr.term == 2
                             select egr).FirstOrDefault();


            var term3Work = from wgr in WorksGrades
                            where wgr.term == 3
                            select wgr;
            var term3Exam = (from egr in ExamGrades
                             where egr.term == 3
                             select egr).FirstOrDefault();


            var term4Work = from wgr in WorksGrades
                            where wgr.term == 4
                            select wgr;
            var term4Exam = (from egr in ExamGrades
                             where egr.term == 4
                             select egr).FirstOrDefault();


            var term1 = new
            {
                workMean = term1Work.Select(t => t.grade).Average(),
                works = term1Work,
                exam = term1Exam
            };

            var term2 = new
            {
                workMean = term2Work.Select(t => t.grade).Average(),
                works = term2Work,
                exam = term2Exam
            };


            var term3 = new
            {
                workMean = term3Work.Select(t => t.grade).Average(),
                works = term3Work,
                exam = term3Exam
            };


            var term4 = new
            {
                workMean = term4Work.Select(t => t.grade).Average(),
                works = term4Work,
                exam = term4Exam
            };

            var data = new
            {
                Subjects = from sbj in dataBaseContext.subjects
                           select new
                           {
                               subjectId = sbj.Id,
                               subjectName = sbj.Name
                           },

                Term1 = term1,
                Term2 = term2,
                Term3 = term3,
                Term4 = term4
            };
            return View(data);
        }

        public IActionResult Index()
        {
            try
            {
                if (StudentId == null || ClassId == null)
                {
                    var s = dataBaseContext.students.FirstOrDefault(s => s.UserId == HttpContext.Session.GetInt32("Id"));
                    StudentId = s.Id;
                    ClassId = dataBaseContext.classes.FirstOrDefault(c => c.Id == s.ClassId).Id;
                }
            }
            catch (Exception e)
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
    }
}
