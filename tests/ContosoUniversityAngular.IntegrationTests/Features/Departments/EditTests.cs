namespace ContosoUniversityAngular.IntegrationTests.Features.Departments
{
    using ContosoUniversityAngular.Features.Departments;
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

            var editCommand = new Edit.Command
            {
                Id = createdDepartment.Id,
                Name = "Mathematics",
                Budget = 1200000.00m,
                Administrator = administrator,
                StartDate = new DateTime(2012, 01, 01),
            };

            await fixture.SendAsync(editCommand);

            //Assert
            var departmentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Departments
                .Include(dep => dep.Administrator)
                .FirstOrDefaultAsync(dep => dep.Id == createdDepartment.Id));

            departmentInDb.ShouldNotBeNull();
            departmentInDb.Name.ShouldBe(editCommand.Name);
            departmentInDb.Budget.ShouldBe(editCommand.Budget);
            departmentInDb.Administrator.FullName.ShouldBe(editCommand.Administrator.FullName);
            departmentInDb.StartDate.ShouldBe(editCommand.StartDate);
        }

        public async Task ResponseReturnsCorrectData(SliceFixture fixture)
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

            var editCommand = new Edit.Command
            {
                Id = createdDepartment.Id,
                Name = "Mathematics",
                Budget = 1200000.00m,
                Administrator = administrator,
                StartDate = new DateTime(2012, 01, 01),
            };

            var response = await fixture.SendAsync(editCommand);

            //Assert
            var departmentInDb = await fixture.ExecuteDbContextAsync(context => context
                .Departments
                .Include(dep => dep.Administrator)
                .FirstOrDefaultAsync(dep => dep.Id == createdDepartment.Id));

            response.ShouldNotBeNull();
            response.Name.ShouldBe(departmentInDb.Name);
            response.Budget.ShouldBe(departmentInDb.Budget);
            response.AdministratorFullName.ShouldBe(departmentInDb.Administrator.FullName);
            response.StartDate.ShouldBe(departmentInDb.StartDate);
        }
    }
}
