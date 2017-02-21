namespace ContosoUniversityAngular.IntegrationTests.Features.Students
{
    using ContosoUniversityAngular.Features.Students;
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
            var student = new Student
            {
                FirstName = "Another",
                LastName = "Student",
                EnrollmentDate = new DateTime(2014, 01, 04)
            };

            await fixture.InsertAsync(student);

            var deleteCommand = new Delete.Command
            {
                Id = student.Id
            };

            //Act
            await fixture.SendAsync(deleteCommand);

            //Assert
            var studentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Students
                .FirstOrDefaultAsync(s => s.Id == student.Id));

            studentInDb.ShouldBeNull();
        }
    }
}
