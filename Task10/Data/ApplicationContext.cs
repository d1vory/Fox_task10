using Microsoft.EntityFrameworkCore;
using Task10.Models;

namespace Task10.Data;

public class ApplicationContext: BaseApplicationContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=o-dubchak-pc2\SQLEXPRESS;initial catalog=students2;trusted_connection=true;TrustServerCertificate=True;");
    }
}