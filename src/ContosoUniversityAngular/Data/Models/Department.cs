namespace ContosoUniversityAngular.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department : IEntity
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int? InstructorId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Instructor Administrator { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
