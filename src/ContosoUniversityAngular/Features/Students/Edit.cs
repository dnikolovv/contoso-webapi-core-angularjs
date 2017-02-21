namespace ContosoUniversityAngular.Features.Students
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

    public class Edit
    {
        public class Command : IAsyncRequest<Response>
        {
            public int? Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime? EnrollmentDate { get; set; }

            public ICollection<Enrollment> Enrollments { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Id).NotNull().NotEmpty();
                RuleFor(c => c.FirstName).NotNull().NotEmpty();
                RuleFor(c => c.LastName).NotNull().NotEmpty();
                RuleFor(c => c.EnrollmentDate).NotNull().NotEmpty();
            }
        }

        public class Response
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime EnrollmentDate { get; set; }
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
                var studentInDb = await _context
                    .Students
                    .Include(s => s.Enrollments)
                    .FirstOrDefaultAsync(s => s.Id == message.Id);

                Mapper.Map(message, studentInDb);

                if (message.Enrollments != null)
                {
                    await UpdateEnrollments(studentInDb, message.Enrollments);
                }

                await _context.SaveChangesAsync();

                return Mapper.Map<Response>(studentInDb);
            }

            private async Task UpdateEnrollments(Student student, ICollection<Enrollment> newEnrollments)
            {
                if (student.Enrollments == null)
                {
                    student.Enrollments = new List<Enrollment>();
                }
                else
                {
                    student.Enrollments.Clear();
                }

                foreach (var enrollment in newEnrollments)
                {
                    var enrollmentInDb = await _context.Enrollments
                        .FirstOrDefaultAsync(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                    if (enrollmentInDb != null)
                    {
                        student.Enrollments.Add(enrollmentInDb);
                    }
                    else
                    {
                        if (enrollment.Student != null)
                        {
                            _context.Students.Attach(enrollment.Student);
                        }

                        if (enrollment.Course != null)
                        {
                            _context.Courses.Attach(enrollment.Course);
                        }

                        _context.Enrollments.Add(enrollment);
                        student.Enrollments.Add(enrollment);
                    }
                }
            }
        }
    }
}
