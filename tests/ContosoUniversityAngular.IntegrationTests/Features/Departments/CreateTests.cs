namespace ContosoUniversityAngular.IntegrationTests.Features.Departments
{
    using ContosoUniversityAngular.Features.Departments;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using System.Threading.Tasks;

    public class CreateTests
    {
        public async Task CanCreate(SliceFixture fixture)
        {
            //Arrange
            var administrator = new Instructor
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new System.DateTime(1953, 01, 10)
            };

            await fixture.InsertAsync(administrator);

            var createDepartmentCommand = new Create.Command
            {
                Name = "Some department",
                Budget = 12000.00m,
                StartDate = new System.DateTime(1953, 01, 10),
                Administrator = administrator
            };

            //Act
            await fixture.SendAsync(createDepartmentCommand);

            //Assert
            var departmentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Departments
                .FirstOrDefaultAsync(d => d.Name == createDepartmentCommand.Name));

            departmentInDb.ShouldNotBeNull();
            departmentInDb.Name.ShouldBe(createDepartmentCommand.Name);
            departmentInDb.StartDate.ShouldBe(createDepartmentCommand.StartDate);
            departmentInDb.Budget.ShouldBe(createDepartmentCommand.Budget);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
        {
            //Arrange
            var administrator = new Instructor
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new System.DateTime(1953, 01, 10)
            };

            await fixture.InsertAsync(administrator);

            var createDepartmentCommand = new Create.Command
            {
                Name = "Some department",
                Budget = 12000.00m,
                StartDate = new System.DateTime(1953, 01, 10),
                Administrator = administrator
            };

            //Act
            var response = await fixture.SendAsync(createDepartmentCommand);

            //Assert
            var departmentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Departments
                .FirstOrDefaultAsync(d => d.Name == createDepartmentCommand.Name));

            response.ShouldNotBeNull();
            response.Id.ShouldBe(departmentInDb.Id);
            response.Name.ShouldBe(departmentInDb.Name);
            response.StartDate.ShouldBe(departmentInDb.StartDate);
            response.Budget.ShouldBe(departmentInDb.Budget);
        }
    }
}
