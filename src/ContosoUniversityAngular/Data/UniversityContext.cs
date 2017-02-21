namespace ContosoUniversityAngular.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Models;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public class UniversityContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<CourseInstructor> CourseInstructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.CourseId, ci.InstructorId });
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch (Exception)
            {
                RollbackTransaction();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
