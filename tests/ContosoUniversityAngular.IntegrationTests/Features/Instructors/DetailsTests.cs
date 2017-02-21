namespace ContosoUniversityAngular.IntegrationTests.Features.Instructors
{
    using ContosoUniversityAngular.Features.Instructors;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DetailsTests
    {
        public async Task CanGetDetails(SliceFixture fixture)
        {
            //Arrange
            var department = new Department
            {
                Name = "Some department"
            };

            var course = new Course
            {
                Title = "Course",
                Credits = 3,
                Department = department
            };

            await fixture.InsertAsync(course);

            var createInstructorCommand = new CreateEdit.Command
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2012, 03, 01),
                SelectedCourses = new List<CourseInstructor>() { new CourseInstructor() { Course = course, CourseId = course.Id } }
            };
            
            var createdInstructor = await fixture.SendAsync(createInstructorCommand);

            //Act
            var detailsQuery = new Details.Query
            {
                Id = createdInstructor.Id
            };

            var response = await fixture.SendAsync(detailsQuery);

            //Assert
            var instructorInDb = await fixture.ExecuteDbContextAsync(context => context
                .Instructors
                .Include(i => i.CourseInstructors)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(i => i.Id == createdInstructor.Id));

            response.ShouldNotBeNull();
            response.Id.ShouldBe(instructorInDb.Id);
            response.FirstName.ShouldBe(instructorInDb.FirstName);
            response.LastName.ShouldBe(instructorInDb.LastName);
            response.HireDate.ShouldBe(instructorInDb.HireDate);

            response.Courses.ElementAt(0).Id
                .ShouldBe(instructorInDb.CourseInstructors.ElementAt(0).CourseId);
            response.Courses.ElementAt(0).Title
                .ShouldBe(instructorInDb.CourseInstructors.ElementAt(0).Course.Title);
        }
    }
}
