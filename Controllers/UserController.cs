using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using SchoolSystem.Models;

namespace SchoolSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly DataBaseContext dataBaseContext;

        public UserController(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            List<User> _users = dataBaseContext.users.ToList();

            return View(_users);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserAuth(string id, string email, string password, string type)
        {
            if (email == "admin@gmail.com" && password == "senhaAdmin" && id == "1")
            {
                return RedirectToAction("Index", "Admin");
            }

            var user = dataBaseContext.users.FirstOrDefault(u => u.Email == email && u.Id == int.Parse(id) && u.PassWordHash == password);

            if (user != null) 
            {
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetString("Type", type);
                return RedirectToAction("Index", type == "professor" ? "Professor" : "Aluno");
            }
            
            return View("~/Views/User/Login.cshtml");
        }
    }
}
