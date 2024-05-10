using Microsoft.EntityFrameworkCore;
using Task10.Models;

namespace Task10.Data;

public class ApplicationContext: DbContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=o-dubchak-pc2\SQLEXPRESS;initial catalog=students2;trusted_connection=true;TrustServerCertificate=True;");
    }
}