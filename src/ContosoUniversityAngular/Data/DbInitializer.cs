namespace ContosoUniversityAngular.Data
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public static class DbInitializer
    {
        public static async Task PopulateDb(IApplicationBuilder app)
        {
            UniversityContext context = (UniversityContext)app.ApplicationServices
                .GetService(typeof(UniversityContext));

            if (!context.People.Any())
            {
                Debug.WriteLine("Detected that the db contains no people. Beginning to initialize...");

                try
                {
                    Debug.WriteLine("Opening transaction.");
                    context.BeginTransaction();

                    Instructor[] instructors = new Instructor[]
                    {
                    new Instructor
                    {
                        FirstName = "John",
                        LastName = "Hilton",
                        HireDate = new DateTime(2012, 10, 10),
                        CourseInstructors = new List<CourseInstructor>()
                    },
                    new Instructor
                    {
                        FirstName = "Mark",
                        LastName = "Twain",
                        HireDate = new DateTime(1904, 10, 10),
                        CourseInstructors = new List<CourseInstructor>()
                    },
                    new Instructor
                    {
                        FirstName = "Jimmy",
                        LastName = "Bogard",
                        HireDate = new DateTime(1736, 9, 10),
                        CourseInstructors = new List<CourseInstructor>()
                    }};

                    context.AddRange(instructors);
                    Debug.WriteLine("Added instructors to the db.");

                    Department[] departments = new Department[]
                    {
                    new Department
                    {
                        Name = "Physics",
                        InstructorId = instructors[0].Id,
                        Administrator = instructors[0],
                        StartDate = new DateTime(2013, 01, 01),
                        Budget = 12903.00m
                    },
                    new Department
                    {
                        Name = "Mathematics",
                        InstructorId = instructors[1].Id,
                        Administrator = instructors[1],
                        StartDate = new DateTime(2013, 04, 01),
                        Budget = 1234.00m
                    },
                    new Department
                    {
                        Name = "Arts",
                        InstructorId = instructors[2].Id,
                        Administrator = instructors[2],
                        StartDate = new DateTime(2013, 06, 01),
                        Budget = 12351.12m
                    }};

                    context.AddRange(departments);
                    Debug.WriteLine("Added departments to the db.");

                    Course[] courses = new Course[]
                    {
                    new Course
                    {
                        Title = "Physics course",
                        DepartmentId = departments[0].Id,
                        Department = departments[0],
                        Credits = 3
                    },
                    new Course
                    {
                        Title = "Mathematics course",
                        DepartmentId = departments[1].Id,
                        Department = departments[1],
                        Credits = 4
                    },
                    new Course
                    {
                        Title = "Arts course",
                        DepartmentId = departments[2].Id,
                        Department = departments[2],
                        Credits = 2
                    }};

                    context.AddRange(courses);

                    Debug.WriteLine("Added courses to the db.");

                    instructors[0].CourseInstructors.Add(new CourseInstructor() { InstructorId = instructors[0].Id, CourseId = courses[0].Id });
                    instructors[1].CourseInstructors.Add(new CourseInstructor() { InstructorId = instructors[1].Id, CourseId = courses[1].Id });
                    instructors[2].CourseInstructors.Add(new CourseInstructor() { InstructorId = instructors[2].Id, CourseId = courses[2].Id });

                    Debug.WriteLine("Added courses to the instructors.");

                    Student[] students = new Student[]
                    {
                    new Student
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        EnrollmentDate = new DateTime(2012, 03, 01)
                    },
                    new Student
                    {
                        FirstName = "Hilary",
                        LastName = "Jameson",
                        EnrollmentDate = new DateTime(2011, 03, 01)
                    },
                    new Student
                    {
                        FirstName = "George",
                        LastName = "Carlos",
                        EnrollmentDate = new DateTime(2013, 03, 01)
                    },
                    new Student
                    {
                        FirstName = "Donald",
                        LastName = "Duck",
                        EnrollmentDate = new DateTime(2011, 05, 21)
                    }};

                    context.AddRange(students);
                    Debug.WriteLine("Added students to the db.");

                    Enrollment[] enrollments = new Enrollment[]
                    {
                    new Enrollment
                    {
                        Course = courses[0],
                        CourseId = courses[0].Id,
                        Student = students[0],
                        StudentId = students[0].Id,
                        Grade = Grade.A
                    },
                    new Enrollment
                    {
                        Course = courses[1],
                        CourseId = courses[1].Id,
                        Student = students[0],
                        StudentId = students[0].Id,
                        Grade = Grade.D
                    },
                    new Enrollment
                    {
                        Course = courses[2],
                        CourseId = courses[2].Id,
                        Student = students[2],
                        StudentId = students[2].Id,
                        Grade = Grade.C
                    },
                    new Enrollment
                    {
                        Course = courses[1],
                        CourseId = courses[1].Id,
                        Student = students[1],
                        StudentId = students[1].Id,
                        Grade = Grade.F
                    },
                    new Enrollment
                    {
                        Course = courses[0],
                        CourseId = courses[0].Id,
                        Student = students[1],
                        StudentId = students[1].Id,
                        Grade = Grade.B
                    }};

                    context.AddRange(enrollments);
                    Debug.WriteLine("Added enrollments to the db.");

                    Debug.WriteLine("Trying to commit transaction...");
                    await context.CommitTransactionAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"There was an exception of type {e.GetType()}. Rolling back transaction...");
                    context.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
