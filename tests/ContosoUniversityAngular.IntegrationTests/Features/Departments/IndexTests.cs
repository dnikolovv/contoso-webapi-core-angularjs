namespace ContosoUniversityAngular.IntegrationTests.Features.Departments
{
    using ContosoUniversityAngular.Features.Departments;
    using Data.Models;
    using Shouldly;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexTests
    {
        public async Task IndexReturnsAllDepartments(SliceFixture fixture)
        {
            //Arrange
            Instructor administrator = new Instructor
            {
                FirstName = "The mighty",
                LastName = "Admin"
            };

            Department[] departmentsToInsert = new Department[]
            {
                new Department
                {
                    Name = "Some department1",
                    Administrator = administrator
                },
                new Department
                {
                    Name = "Some department2",
                    Administrator = administrator
                },
                new Department
                {
                    Name = "Some department3",
                    Administrator = administrator
                }
            };

            await fixture.InsertAsync(departmentsToInsert);

            var indexQuery = new Index.Query();

            //Act
            var response = await fixture.SendAsync(indexQuery);

            //Assert
            response.Departments.Count.ShouldBe(departmentsToInsert.Length);
            response.Departments.ElementAt(0).Name.ShouldBe(departmentsToInsert[0].Name);
            response.Departments.ElementAt(1).Name.ShouldBe(departmentsToInsert[1].Name);
            response.Departments.ElementAt(2).Name.ShouldBe(departmentsToInsert[2].Name);
        }
    }
}
