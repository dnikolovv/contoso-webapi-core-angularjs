namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses;
    using Data.Models;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class DetailsTests
    {
        public async Task ReturnsCorrectInformation(SliceFixture fixture)
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
            var detailsQuery = new Details.Query
            {
                Id = createdCourse.Id
            };

            var response = await fixture.SendAsync(detailsQuery);

            //Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(createdCourse.Id);
            response.Title.ShouldBe(createdCourse.Title);
            response.Credits.ShouldBe(createdCourse.Credits);
            response.Department.Name.ShouldBe(createdCourse.DepartmentName);
        }
    }
}
