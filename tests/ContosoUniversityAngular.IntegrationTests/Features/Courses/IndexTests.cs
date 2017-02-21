namespace ContosoUniversityAngular.IntegrationTests.Features.Courses
{
    using ContosoUniversityAngular.Features.Courses;
    using Data.Models;
    using Shouldly;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexTests
    {
        public async Task IndexReturnsAllCourses(SliceFixture fixture)
        {
            // Arrange
            Department someDept = new Department()
            {
                Name = "Some department"
            };

            Course[] coursesToInsert = new Course[]
            {
                new Course
                {
                    Title = "Some course1",
                    Department = someDept
                },
                new Course
                {
                    Title = "Some course2",
                    Department = someDept
                },
                new Course
                {
                    Title = "Some course3",
                    Department = someDept
                },
            };

            await fixture.InsertAsync(coursesToInsert);

            var indexQuery = new Index.Query
            {
                SelectedDepartmentName = null
            };

            // Act
            var response = await fixture.SendAsync(indexQuery);

            // Assert
            response.SelectedDepartmentName.ShouldBeNull();
            response.Courses.Count.ShouldBe(coursesToInsert.Length);

            for (int i = 0; i < coursesToInsert.Length; i++)
            {
                response.Courses.ElementAt(i).Title.ShouldBe(coursesToInsert[i].Title);
                response.Courses.ElementAt(i).DepartmentName.ShouldBe(coursesToInsert[i].Department.Name);
            }
        }

        public async Task IndexCanFilterByDepartment(SliceFixture fixture)
        {
            // Arrange
            Department firstDepartment = new Department()
            {
                Name = "First department"
            };

            Department secondDepartment = new Department()
            {
                Name = "Second department"
            };

            Course[] coursesToInsert = new Course[]
            {
                new Course
                {
                    Title = "Some course1",
                    Department = firstDepartment
                },
                new Course
                {
                    Title = "Some course2",
                    Department = firstDepartment
                },
                new Course
                {
                    Title = "Some course3",
                    Department = firstDepartment
                },
                new Course
                {
                    Title = "Some course4",
                    Department = secondDepartment
                },
                new Course
                {
                    Title = "Some course5",
                    Department = secondDepartment
                },
                new Course
                {
                    Title = "Some course6",
                    Department = secondDepartment
                },
            };

            await fixture.InsertAsync(coursesToInsert);

            var indexQuery = new Index.Query
            {
                SelectedDepartmentName = secondDepartment.Name
            };

            // Act
            var response = await fixture.SendAsync(indexQuery);

            // Assert
            response.SelectedDepartmentName.ShouldBe(secondDepartment.Name);
            response.Courses.Count.ShouldBe(coursesToInsert.Where(c => c.Department == secondDepartment).Count());

            response.Courses.ElementAt(0).Title.ShouldBe(coursesToInsert[3].Title);
            response.Courses.ElementAt(1).Title.ShouldBe(coursesToInsert[4].Title);
            response.Courses.ElementAt(2).Title.ShouldBe(coursesToInsert[5].Title);

            response.Courses.ElementAt(0).DepartmentName.ShouldBe(coursesToInsert[3].Department.Name);
            response.Courses.ElementAt(1).DepartmentName.ShouldBe(coursesToInsert[4].Department.Name);
            response.Courses.ElementAt(2).DepartmentName.ShouldBe(coursesToInsert[5].Department.Name);
        }
    }
}
