namespace ContosoUniversityAngular.IntegrationTests.Features.Students
{
    using ContosoUniversityAngular.Features.Students;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class CreateTests
    {
        public async Task CanCreate(SliceFixture fixture)
        {
            //Arrange
            var createCommand = new Create.Command
            {
                FirstName = "Some",
                LastName = "Student",
                EnrollmentDate = new DateTime(2013, 03, 04)
            };

            //Act
            var createdStudent = await fixture.SendAsync(createCommand);

            //Assert
            var studentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Students
                .FirstOrDefaultAsync(s => s.Id == createdStudent.Id));

            studentInDb.ShouldNotBeNull();
            studentInDb.FirstName.ShouldBe(createCommand.FirstName);
            studentInDb.LastName.ShouldBe(createCommand.LastName);
            studentInDb.EnrollmentDate.ShouldBe((DateTime)createCommand.EnrollmentDate);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
        {
            //Arrange
            var createCommand = new Create.Command
            {
                FirstName = "Some",
                LastName = "Student",
                EnrollmentDate = new DateTime(2013, 03, 04)
            };

            //Act
            var response = await fixture.SendAsync(createCommand);

            //Assert
            var studentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Students
                .FirstOrDefaultAsync(s => s.Id == response.Id));

            studentInDb.ShouldNotBeNull();
            studentInDb.FirstName.ShouldBe(response.FirstName);
            studentInDb.LastName.ShouldBe(response.LastName);
            studentInDb.EnrollmentDate.ShouldBe(response.EnrollmentDate);
        }
    }
}
