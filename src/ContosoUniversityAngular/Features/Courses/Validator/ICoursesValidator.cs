namespace ContosoUniversityAngular.Features.Courses.Validator
{
    public interface ICoursesValidator
    {
        bool DepartmentExistsInDb(string departmentName);
    }
}
