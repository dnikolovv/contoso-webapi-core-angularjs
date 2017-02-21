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

    public class CreateTests
    {
        public async Task CanCreate(SliceFixture fixture)
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

            //Act
            var createdInstructor = await fixture.SendAsync(createInstructorCommand);

            //Assert
            var instructorInDb = await fixture.ExecuteDbContextAsync(context => context
                .Instructors
                .Include(i => i.CourseInstructors)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == createdInstructor.Id));

            instructorInDb.Id.ShouldBe(createdInstructor.Id);
            instructorInDb.FirstName.ShouldBe(createdInstructor.FirstName);
            instructorInDb.LastName.ShouldBe(createdInstructor.LastName);
            instructorInDb.HireDate.ShouldBe(createdInstructor.HireDate);
            instructorInDb.CourseInstructors.Count.ShouldBe(createInstructorCommand.SelectedCourses.Count);
            instructorInDb.CourseInstructors.ElementAt(0).CourseId.ShouldBe(createInstructorCommand.SelectedCourses.ElementAt(0).CourseId);
            instructorInDb.CourseInstructors.ElementAt(0).Course.Title.ShouldBe(createInstructorCommand.SelectedCourses.ElementAt(0).Course.Title);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
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

            //Act
            var response = await fixture.SendAsync(createInstructorCommand);

            //Assert
            var instructorInDb = await fixture.ExecuteDbContextAsync(context => context
                .Instructors
                .Include(i => i.CourseInstructors)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == response.Id));

            response.Id.ShouldBe(instructorInDb.Id);
            response.FirstName.ShouldBe(instructorInDb.FirstName);
            response.LastName.ShouldBe(instructorInDb.LastName);
            response.HireDate.ShouldBe(instructorInDb.HireDate);
        }
    }
}
