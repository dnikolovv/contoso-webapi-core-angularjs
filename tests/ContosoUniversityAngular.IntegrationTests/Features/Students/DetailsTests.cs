namespace ContosoUniversityAngular.IntegrationTests.Features.Students
{
    using ContosoUniversityAngular.Features.Students;
    using Data.Models;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class DetailsTests
    {
        public async Task CanGetDetails(SliceFixture fixture)
        {
            //Arrange
            var student = new Student
            {
                FirstName = "Another",
                LastName = "Student",
                EnrollmentDate = new DateTime(2014, 01, 04)
            };

            await fixture.InsertAsync(student);

            var detailsQuery = new Details.Query
            {
                Id = student.Id
            };

            //Act
            var response = await fixture.SendAsync(detailsQuery);

            //Assert
            response.ShouldNotBeNull();
            response.FirstName.ShouldBe(student.FirstName);
            response.LastName.ShouldBe(student.LastName);
            response.EnrollmentDate.ShouldBe(student.EnrollmentDate);
        }
    }
}
