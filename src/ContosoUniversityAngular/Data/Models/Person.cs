namespace ContosoUniversityAngular.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Person : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name should not be longer than 50 symbols.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name should not be longer than 50 symbols")]
        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
    }
}
