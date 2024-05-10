using Microsoft.EntityFrameworkCore;
using Task10.Models;

namespace Task10.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var course1 = new Course {Id=1, Name = "Python Programming Fundamentals", Description = "Learn the basics of Python programming language, including variables, data types, control structures, functions, and basic algorithms." };
        var course2 = new Course {Id=2, Name = "Digital Marketing Strategies", Description = "Discover effective digital marketing strategies, including search engine optimization (SEO), social media marketing, email marketing, and content marketing." };
        var course3 = new Course {Id=3, Name = "Web Development Basics", Description = "Get started with web development by learning HTML, CSS, and JavaScript fundamentals." };
        var course4 = new Course {Id=4, Name = "Graphic Design Essentials", Description = "Learn the fundamentals of graphic design, including typography, color theory, layout design, and image manipulation techniques." };
        var course5 = new Course {Id=5, Name = "Mobile App Development Basics", Description = "Get started with mobile app development by learning about mobile platforms, user interface design, and mobile app development tools." };
        
        modelBuilder.Entity<Course>().HasData(course1, course2, course3, course4, course5);
        
        var group1 = new Group { Id = 1, Name = "PPF-01", CourseId = 1, };
        var group2 = new Group { Id = 2, Name = "PPF-02", CourseId = 1, };
        var group3 = new Group { Id = 3, Name = "PPF-03", CourseId = 1, };
        var group4 = new Group { Id = 4, Name = "DMC-01", CourseId = 2, };
        var group5 = new Group { Id = 5, Name = "DMC-02", CourseId = 2, };
        var group6 = new Group { Id = 6, Name = "WDB-01", CourseId = 3, };
        var group7 = new Group { Id = 7, Name = "WDB-02", CourseId = 3, };
        var group8 = new Group { Id = 8, Name = "GDE-01", CourseId = 4, };

        modelBuilder.Entity<Group>().HasData(group1, group2, group3, group4, group5, group6, group7, group8);

        var student1 = new Student { Id = 1, FirstName = "John", LastName = "Doe", GroupId = 1 };
        var student2 = new Student { Id = 2, FirstName = "Alice", LastName = "Smith", GroupId = 1 };
        var student3 = new Student { Id = 3, FirstName = "Michael", LastName = "Johnson", GroupId = 1 };
        var student4 = new Student { Id = 4, FirstName = "Emily", LastName = "Brown", GroupId = 2 };
        var student5 = new Student { Id = 5, FirstName = "Daniel", LastName = "Wilson", GroupId = 3 };
        var student6 = new Student { Id = 6, FirstName = "Jessica", LastName = "Martinez", GroupId = 3 };
        var student7 = new Student { Id = 7, FirstName = "Matthew", LastName = "Taylor", GroupId = 4 };
        var student8 = new Student { Id = 8, FirstName = "Sophia", LastName = "Anderson", GroupId = 5 };
        var student9 = new Student { Id = 9, FirstName = "William", LastName = "Thomas", GroupId = 6 };
        var student10 = new Student { Id = 10, FirstName = "Olivia", LastName = "Hernandez", GroupId = 7 };
        var student11 = new Student { Id = 11, FirstName = "Ethan", LastName = "Moore", GroupId = 8 };

        modelBuilder.Entity<Student>().HasData(student1, student2, student3, student4, student5, student6, student7,
            student8, student9, student10, student11
        );
    }
}