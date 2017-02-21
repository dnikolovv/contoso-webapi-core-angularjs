namespace ContosoUniversityAngular.Data.Models
{
    public class CourseInstructor : IEntity
    {
        // Primary key is set in OnModelCreating

        public virtual int CourseId { get; set; }

        public virtual int InstructorId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Instructor Instructor { get; set; }
    }

}