namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class CreateTests
    {
        public async Task CanCreate(SliceFixture fixture)
        {
            //Arrange
            var department = new Department
            {
                Name = "Physics",
                StartDate = new DateTime(2013, 01, 01),
                Budget = 12903.00m
            };

            await fixture.InsertAsync(department);

            var createCommand = new Create.Command
            {
                Title = "Title",
                Credits = 3,
                Department = department
            };

            //Act
            await fixture.SendAsync(createCommand);

            //Assert
            var courseInDb = await fixture.ExecuteDbContextAsync(context => context
                .Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Title == createCommand.Title));

            courseInDb.ShouldNotBeNull();
            courseInDb.Title.ShouldBe(createCommand.Title);
            courseInDb.Credits.ShouldBe(createCommand.Credits);
            courseInDb.Department.Budget.ShouldBe(department.Budget);
            courseInDb.Department.Name.ShouldBe(department.Name);
            courseInDb.Department.StartDate.ShouldBe(department.StartDate);
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

            var createCommand = new Create.Command
            {
                Title = "Title",
                Credits = 3,
                Department = department
            };

            //Act
            var response = await fixture.SendAsync(createCommand);

            //Assert
            var courseInDb = await fixture.ExecuteDbContextAsync(context => context
                .Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Title == createCommand.Title));

            response.Id.ShouldBe(courseInDb.Id);
            response.Title.ShouldBe(courseInDb.Title);
            response.Credits.ShouldBe(courseInDb.Credits);
            response.DepartmentName.ShouldBe(courseInDb.Department.Name);
        }
    }
}
