namespace ContosoUniversityAngular.IntegrationTests.Features.Instructors
{
    using ContosoUniversityAngular.Features.Instructors;
    using Data.Models;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexTests
    {
        public async Task ListsAllInstructors(SliceFixture fixture)
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

            var createInstructorsCommands = new CreateEdit.Command[]
            {
                new CreateEdit.Command
                {
                    FirstName = "John",
                    LastName = "Smith",
                    HireDate = new DateTime(2012, 03, 01),
                    SelectedCourses = new List<CourseInstructor>() { new CourseInstructor() { CourseId = course.Id, Course = course } }
                },
                new CreateEdit.Command
                {
                    FirstName = "Marcus",
                    LastName = "Ruhl",
                    HireDate = new DateTime(2011, 04, 01),
                    SelectedCourses = new List<CourseInstructor>() { new CourseInstructor() { CourseId = course.Id, Course = course } }
                },
            };

            foreach (var command in createInstructorsCommands)
            {
                await fixture.SendAsync(command);
            }

            var indexQuery = new Index.Query();

            //Act
            var response = await fixture.SendAsync(indexQuery);

            //Assert
            response.ShouldNotBeNull();
            response.Instructors.Count.ShouldBe(createInstructorsCommands.Length);
            response.Instructors.ElementAt(0).FirstName.ShouldBe(createInstructorsCommands[0].FirstName);
            response.Instructors.ElementAt(0).LastName.ShouldBe(createInstructorsCommands[0].LastName);
            response.Instructors.ElementAt(0).HireDate.ShouldBe((DateTime)createInstructorsCommands[0].HireDate);
            response.Instructors.ElementAt(0).Courses.Count.ShouldBe(createInstructorsCommands[0].SelectedCourses.Count);
            response.Instructors.ElementAt(0).Courses.ElementAt(0).Title
                .ShouldBe(createInstructorsCommands[0].SelectedCourses.ElementAt(0).Course.Title);

            response.Instructors.ElementAt(1).FirstName.ShouldBe(createInstructorsCommands[1].FirstName);
            response.Instructors.ElementAt(1).LastName.ShouldBe(createInstructorsCommands[1].LastName);
            response.Instructors.ElementAt(1).HireDate.ShouldBe((DateTime)createInstructorsCommands[1].HireDate);
            response.Instructors.ElementAt(1).Courses.Count.ShouldBe(createInstructorsCommands[1].SelectedCourses.Count);
            response.Instructors.ElementAt(1).Courses.ElementAt(0).Title
                .ShouldBe(createInstructorsCommands[1].SelectedCourses.ElementAt(0).Course.Title);
        }
    }
}
