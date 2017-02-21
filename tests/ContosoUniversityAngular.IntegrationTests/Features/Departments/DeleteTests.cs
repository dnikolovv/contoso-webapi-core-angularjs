namespace ContosoUniversityAngular.IntegrationTests.Features.Departments
{
    using ContosoUniversityAngular.Features.Departments;
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
            var deleteCommand = new Delete.Command
            {
                Id = createdDepartment.Id
            };

            await fixture.SendAsync(deleteCommand);

            //Assert
            var depInDb = await fixture.ExecuteDbContextAsync(context => context
                .Departments
                .FirstOrDefaultAsync(d => d.Id == createdDepartment.Id));

            depInDb.ShouldBeNull();
        }
    }
}
