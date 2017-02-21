namespace ContosoUniversityAngular.IntegrationTests.Features.Students
{
    using ContosoUniversityAngular.Features.Students;
    using Data.Models;
    using Shouldly;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexTests
    {
        public async Task ListsAllStudents(SliceFixture fixture)
        {
            //Arrange
            var students = new Student[]
            {
                new Student
                {
                    FirstName = "John",
                    LastName = "Smith",
                    EnrollmentDate = new DateTime(2012, 01, 03)
                },
                new Student
                {
                    FirstName = "Carlos",
                    LastName = "Rodrigez",
                    EnrollmentDate = new DateTime(2012, 01, 03)
                },
                new Student
                {
                    FirstName = "Peter",
                    LastName = "Pan",
                    EnrollmentDate = new DateTime(2012, 01, 03)
                },
            };

            await fixture.InsertAsync(students);

            //Act
            var indexQuery = new Index.Query();

            var response = await fixture.SendAsync(indexQuery);

            //Assert
            response.Students.Count.ShouldBe(students.Length);
            response.Students.ElementAt(0).FullName.ShouldBe(students[0].FullName);
            response.Students.ElementAt(1).FullName.ShouldBe(students[1].FullName);
            response.Students.ElementAt(2).FullName.ShouldBe(students[2].FullName);
        }
    }
}
