namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class EditTests
    {
        public async Task CanEdit(SliceFixture fixture)
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

            var anotherDepartment = new Department
            {
                Name = "Mathematics",
                StartDate = new DateTime(2011, 01, 01),
                Budget = 12912.00m
            };

            await fixture.InsertAsync(anotherDepartment);

            //Act
            var editCommand = new Edit.Command
            {
                Id = createdCourse.Id,
                Credits = 5,
                Title = "New course title",
                Department = anotherDepartment
            };

            await fixture.SendAsync(editCommand);

            //Assert
            var courseInDb = await fixture.ExecuteDbContextAsync(context => context
                .Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Title == editCommand.Title));

            courseInDb.Id.ShouldBe(editCommand.Id);
            courseInDb.Title.ShouldBe(editCommand.Title);
            courseInDb.Credits.ShouldBe((int)editCommand.Credits);
            courseInDb.Department.Name.ShouldBe(editCommand.Department.Name);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
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

            var anotherDepartment = new Department
            {
                Name = "Mathematics",
                StartDate = new DateTime(2011, 01, 01),
                Budget = 12912.00m
            };

            await fixture.InsertAsync(anotherDepartment);

            //Act
            var editCommand = new Edit.Command
            {
                Id = createdCourse.Id,
                Credits = 5,
                Title = "New course title",
                Department = anotherDepartment
            };

            var editedCourse = await fixture.SendAsync(editCommand);

            //Assert
            editedCourse.Id.ShouldBe(editCommand.Id);
            editedCourse.Title.ShouldBe(editCommand.Title);
            editedCourse.Credits.ShouldBe((int)editCommand.Credits);
            editedCourse.DepartmentName.ShouldBe(editCommand.Department.Name);
        }
    }
}
