namespace ContosoUniversityAngular.Features.Courses.Validator
{
    using Data;
    using System.Linq;

    public class CoursesValidator : ICoursesValidator
    {
        public CoursesValidator(UniversityContext context)
        {
            _context = context;
        }

        private readonly UniversityContext _context;

        public bool DepartmentExistsInDb(string departmentName)
        {
            return _context.Departments
                .FirstOrDefault(d => d.Name == departmentName) != null;
        }
    }
}
