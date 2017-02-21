namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class DeleteTests
    {
        public async Task CanDelete(SliceFixture fixture)
        {
            //Arrange
            var department = new Department
            {
                Name = "Physics",
                StartDate = new DateTime(2013, 01, 01),
                Budget = 12903.00m
            };

            await fixture.InsertAsync(department);

            var createCourseCommand = new Create.Command
            {
                Title = "New course",
                Credits = 3,
                Department = department
            };

            var createdCourse = await fixture.SendAsync(createCourseCommand);

            //Act
            var deleteCommand = new Delete.Command
            {
                Id = createdCourse.Id
            };

            await fixture.SendAsync(deleteCommand);

            //Assert
            var courseInDb = await fixture.ExecuteDbContextAsync(context => context
                .Courses
                .FirstOrDefaultAsync(c => c.Id == createdCourse.Id));

            courseInDb.ShouldBeNull();
        }
    }
}
