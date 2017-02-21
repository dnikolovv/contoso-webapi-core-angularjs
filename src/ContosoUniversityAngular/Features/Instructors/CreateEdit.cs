namespace ContosoUniversityAngular.Features.Instructors
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CreateEdit
    {
        public class Command : IAsyncRequest<Response>
        {
            public int? Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime? HireDate { get; set; }

            public ICollection<CourseInstructor> AvailableCourses { get; set; }

            public ICollection<CourseInstructor> SelectedCourses { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.FirstName).NotNull().NotEmpty();
                RuleFor(c => c.LastName).NotNull().NotEmpty();
                RuleFor(c => c.HireDate).NotNull().NotEmpty();
            }
        }

        public class Response
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime HireDate { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Response>
        {
            public CommandHandler(UniversityContext context)
            {
                _context = context;
            }

            private readonly UniversityContext _context;

            public async Task<Response> Handle(Command message)
            {
                Instructor instructor;

                if (message.Id == null)
                {
                    instructor = Mapper.Map<Instructor>(message);
                    _context.Instructors.Add(instructor);
                }
                else
                {
                    instructor = await _context
                        .Instructors
                        .Include(i => i.CourseInstructors)
                            .ThenInclude(c => c.Course)
                        .FirstOrDefaultAsync(i => i.Id == message.Id);

                    Mapper.Map(message, instructor);
                }

                if (message.SelectedCourses != null)
                {
                    await UpdateCourseInstructors(instructor, message.SelectedCourses);
                }

                await _context.SaveChangesAsync();
                return Mapper.Map<Response>(instructor);
            }

            private async Task UpdateCourseInstructors(Instructor instructor, ICollection<CourseInstructor> newCourseInstructors)
            {
                if (instructor.CourseInstructors == null)
                {
                    instructor.CourseInstructors = new List<CourseInstructor>();
                }
                else
                {
                    instructor.CourseInstructors.Clear();
                }

                foreach (var courseInstructor in newCourseInstructors)
                {
                    var courseInstructorInDb = await _context
                        .CourseInstructors
                            .Include(ci => ci.Course)
                        .FirstOrDefaultAsync(ci => ci.CourseId == courseInstructor.CourseId && ci.InstructorId == courseInstructor.InstructorId);

                    if (courseInstructorInDb != null)
                    {
                        instructor.CourseInstructors.Add(courseInstructorInDb);
                    }
                    else
                    {
                        // To prevent IDENTITY_INSERT problems
                        if (courseInstructor.Course != null)
                        {
                            _context.Courses.Attach(courseInstructor.Course);
                        }

                        if (courseInstructor.Instructor != null)
                        {
                            _context.Instructors.Attach(courseInstructor.Instructor);
                        }

                        _context.CourseInstructors.Add(courseInstructor);
                        instructor.CourseInstructors.Add(courseInstructor);                       
                    }
                }
            }
        }
    }
}
