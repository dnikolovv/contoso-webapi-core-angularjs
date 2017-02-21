namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses.Validator;
    using Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class ValidatorTests
    {
        public async Task CorrectlyDetectsIfDepartmentExists(SliceFixture fixture)
        {
            //Arrange
            var department = new Department
            {
                Name = "Physics",
                StartDate = new DateTime(2013, 01, 01),
                Budget = 12903.00m
            };

            await fixture.InsertAsync(department);

            //Act
            bool departmentExists = false;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            await fixture.ExecuteScopeAsync(async sp =>
            {
                departmentExists = sp.GetService<ICoursesValidator>()
                    .DepartmentExistsInDb(department.Name);
            });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            //Assert
            departmentExists.ShouldBeTrue();
        }
    }
}
