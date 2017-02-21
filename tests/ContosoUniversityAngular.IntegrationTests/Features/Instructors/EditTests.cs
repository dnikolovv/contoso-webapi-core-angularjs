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

    public class EditTests
    {
        public async Task CanEdit(SliceFixture fixture)
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

            var anotherDepartment = new Department
            {
                Name = "Another department"
            };

            var anotherCourse = new Course
            {
                Title = "Another course",
                Credits = 2,
                Department = anotherDepartment
            };

            await fixture.InsertAsync(course);
            await fixture.InsertAsync(anotherCourse);

            var createInstructorCommand = new CreateEdit.Command
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2012, 03, 01),
                SelectedCourses = new List<CourseInstructor>()
                    { new CourseInstructor() { Course = course, CourseId = course.Id } }
            };

            var createdInstructor = await fixture.SendAsync(createInstructorCommand);

            //Act
            var editCommand = new CreateEdit.Command
            {
                Id = createdInstructor.Id,
                FirstName = "Carlos",
                LastName = "Mocha",
                HireDate = new DateTime(2011, 05, 01),
                SelectedCourses = new List<CourseInstructor>()
                    { new CourseInstructor() { Course = anotherCourse, CourseId = anotherCourse.Id } }
            };

            await fixture.SendAsync(editCommand);

            //Assert
            var instructorInDb = await fixture.ExecuteDbContextAsync(context => context
                .Instructors
                .Include(i => i.CourseInstructors)
                    .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(i => i.Id == createdInstructor.Id));

            instructorInDb.ShouldNotBeNull();
            instructorInDb.Id.ShouldBe(createdInstructor.Id);
            instructorInDb.FirstName.ShouldBe(editCommand.FirstName);
            instructorInDb.LastName.ShouldBe(editCommand.LastName);
            instructorInDb.HireDate.ShouldBe((DateTime)editCommand.HireDate);
            instructorInDb.CourseInstructors.ElementAt(0).Course.Title.ShouldBe(anotherCourse.Title);
            instructorInDb.CourseInstructors.ElementAt(0).Course.Credits.ShouldBe(anotherCourse.Credits);
            instructorInDb.CourseInstructors.ElementAt(0).Course.DepartmentId.ShouldBe(anotherCourse.DepartmentId);
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

            var anotherDepartment = new Department
            {
                Name = "Another department"
            };

            var anotherCourse = new Course
            {
                Title = "Another course",
                Credits = 2,
                Department = anotherDepartment
            };

            await fixture.InsertAsync(course);
            await fixture.InsertAsync(anotherCourse);

            var createInstructorCommand = new CreateEdit.Command
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2012, 03, 01),
                SelectedCourses = new List<CourseInstructor>()
                    { new CourseInstructor() { Course = course, CourseId = course.Id } }
            };

            var createdInstructor = await fixture.SendAsync(createInstructorCommand);

            //Act
            var editCommand = new CreateEdit.Command
            {
                Id = createdInstructor.Id,
                FirstName = "Carlos",
                LastName = "Mocha",
                HireDate = new DateTime(2011, 05, 01),
                SelectedCourses = new List<CourseInstructor>()
                    { new CourseInstructor() { Course = anotherCourse, CourseId = anotherCourse.Id } }
            };

            var response = await fixture.SendAsync(editCommand);

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
        }
    }
}
