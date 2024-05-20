using Microsoft.EntityFrameworkCore;
using Task10.Models;

namespace Task10.Data;

public class BaseApplicationContext: DbContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}