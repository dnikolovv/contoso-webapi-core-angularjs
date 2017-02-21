namespace ContosoUniversityAngular.IntegrationTests.Features.Departments
{
    using ContosoUniversityAngular.Features.Departments;
    using Data.Models;
    using Shouldly;
    using System;
    using System.Threading.Tasks;

    public class DetailsTests
    {
        public async Task CanGetDetails(SliceFixture fixture)
        {
            //Arrange
            var administrator = new Instructor
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(1953, 01, 10)
            };

            await fixture.InsertAsync(administrator);

            var createDepartmentComand = new Create.Command
            {
                Name = "Physics",
                StartDate = new DateTime(2013, 01, 01),
                Budget = 12903.00m,
                Administrator = administrator
            };

            var createdDepartment = await fixture.SendAsync(createDepartmentComand);

            //Act
            var detailsQuery = new Details.Query
            {
                Id = createdDepartment.Id
            };

            var response = await fixture.SendAsync(detailsQuery);

            //Assert
            response.Id.ShouldBe(createdDepartment.Id);
            response.Name.ShouldBe(createdDepartment.Name);
            response.StartDate.ShouldBe(createdDepartment.StartDate);
            response.Budget.ShouldBe(createdDepartment.Budget);
            response.Administrator.FullName.ShouldBe(createdDepartment.AdministratorFullName);
        }
    }
}
