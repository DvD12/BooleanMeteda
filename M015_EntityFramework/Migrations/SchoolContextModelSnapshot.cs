﻿// <auto-generated />
using M015_EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace M015_EntityFramework.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesCourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentId")
                        .HasColumnType("int");

                    b.HasKey("CoursesCourseId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("M015_EntityFramework.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("M015_EntityFramework.CourseImage", b =>
                {
                    b.Property<int>("CourseImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseImageId"));

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("CourseImageId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("CourseImages");
                });

            modelBuilder.Entity("M015_EntityFramework.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("StudentId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("M015_EntityFramework.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("student");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("M015_EntityFramework.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("M015_EntityFramework.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("M015_EntityFramework.CourseImage", b =>
                {
                    b.HasOne("M015_EntityFramework.Course", "Course")
                        .WithOne("CourseImage")
                        .HasForeignKey("M015_EntityFramework.CourseImage", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("M015_EntityFramework.Review", b =>
                {
                    b.HasOne("M015_EntityFramework.Student", "Student")
                        .WithMany("Reviews")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("M015_EntityFramework.Course", b =>
                {
                    b.Navigation("CourseImage")
                        .IsRequired();
                });

            modelBuilder.Entity("M015_EntityFramework.Student", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
