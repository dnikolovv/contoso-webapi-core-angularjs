using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ContosoUniversityAngular.Data;

namespace ContosoUniversityAngular.Migrations
{
    [DbContext(typeof(UniversityContext))]
    partial class UniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Credits");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.CourseInstructor", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorId");

                    b.HasKey("CourseId", "InstructorId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CourseInstructors");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budget");

                    b.Property<int?>("InstructorId");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorId");

                    b.Property<string>("Location")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("InstructorId");

                    b.HasIndex("InstructorId")
                        .IsUnique();

                    b.ToTable("OfficeAssignments");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Instructor", b =>
                {
                    b.HasBaseType("ContosoUniversityAngular.Data.Models.Person");

                    b.Property<DateTime>("HireDate");

                    b.ToTable("Instructor");

                    b.HasDiscriminator().HasValue("Instructor");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Student", b =>
                {
                    b.HasBaseType("ContosoUniversityAngular.Data.Models.Person");

                    b.Property<DateTime>("EnrollmentDate");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Course", b =>
                {
                    b.HasOne("ContosoUniversityAngular.Data.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.CourseInstructor", b =>
                {
                    b.HasOne("ContosoUniversityAngular.Data.Models.Course", "Course")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversityAngular.Data.Models.Instructor", "Instructor")
                        .WithMany("CourseInstructors")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Department", b =>
                {
                    b.HasOne("ContosoUniversityAngular.Data.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.Enrollment", b =>
                {
                    b.HasOne("ContosoUniversityAngular.Data.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversityAngular.Data.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversityAngular.Data.Models.OfficeAssignment", b =>
                {
                    b.HasOne("ContosoUniversityAngular.Data.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("ContosoUniversityAngular.Data.Models.OfficeAssignment", "InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
