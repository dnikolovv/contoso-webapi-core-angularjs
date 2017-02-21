namespace ContosoUniversityAngular.IntegrationTests.Features.Students
{
    using ContosoUniversityAngular.Features.Students;
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
            var course = new Course
            {
                Title = "Course",
                Credits = 4,
                Department = new Department
                {
                    Name = "Department"
                }
            };

            var student = new Student
            {
                FirstName = "Another",
                LastName = "Student",
                EnrollmentDate = new DateTime(2014, 01, 04)
            };

            await fixture.InsertAsync(course);
            await fixture.InsertAsync(student);

            var editCommand = new Edit.Command
            {
                Id = student.Id,
                FirstName = "Modified",
                LastName = "Name",
                EnrollmentDate = new DateTime(2013, 01, 04),
                Enrollments = new List<Enrollment>
                {
                    new Enrollment
                    {
                        Course = course,
                        CourseId = course.Id,
                        StudentId = student.Id
                    }
                }
            };

            //Act
            var editedStudent = await fixture.SendAsync(editCommand);

            //Assert
            var studentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == editedStudent.Id));

            studentInDb.ShouldNotBeNull();
            studentInDb.FirstName.ShouldBe(editCommand.FirstName);
            studentInDb.LastName.ShouldBe(editCommand.LastName);
            studentInDb.EnrollmentDate.ShouldBe((DateTime)editCommand.EnrollmentDate);
            studentInDb.Enrollments.Count.ShouldBe(1);
            studentInDb.Enrollments.ElementAt(0).Course.Title.ShouldBe(editCommand.Enrollments.ElementAt(0).Course.Title);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
        {
            //Arrange
            var student = new Student
            {
                FirstName = "Another",
                LastName = "Student",
                EnrollmentDate = new DateTime(2014, 01, 04)
            };

            await fixture.InsertAsync(student);

            var editCommand = new Edit.Command
            {
                Id = student.Id,
                FirstName = "Modified",
                LastName = "Name",
                EnrollmentDate = new DateTime(2013, 01, 04)
            };

            //Act
            var response = await fixture.SendAsync(editCommand);

            //Assert
            var studentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Students
                .FirstOrDefaultAsync(s => s.Id == response.Id));

            response.ShouldNotBeNull();
            response.FirstName.ShouldBe(editCommand.FirstName);
            response.LastName.ShouldBe(editCommand.LastName);
            response.EnrollmentDate.ShouldBe((DateTime)editCommand.EnrollmentDate);
        }
    }
}
