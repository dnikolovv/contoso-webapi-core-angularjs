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

    public class DeleteTests
    {
        public async Task CanDelete(SliceFixture fixture)
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
                SelectedCourses = new List<CourseInstructor>() { new CourseInstructor() { Course = course, Instructor = new Instructor() } }
            };
            
            var createdInstructor = await fixture.SendAsync(createInstructorCommand);

            //Act
            var deleteCommand = new Delete.Command
            {
                Id = createdInstructor.Id
            };

            await fixture.SendAsync(deleteCommand);

            //Assert
            var instructorInDb = await fixture.ExecuteDbContextAsync(context => context
                .Instructors
                .FirstOrDefaultAsync(i => i.Id == createdInstructor.Id));

            instructorInDb.ShouldBeNull();
        }
    }
}
