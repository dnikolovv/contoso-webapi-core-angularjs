namespace ContosoUniversityAngular.Features.Home
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        
        public ViewResult Courses()
        {
            return View();
        }

        public ViewResult Departments()
        {
            return View();
        }

        public ViewResult Students()
        {
            return View();
        }
        
        public ViewResult Instructors()
        {
            return View();
        }
    }
}
