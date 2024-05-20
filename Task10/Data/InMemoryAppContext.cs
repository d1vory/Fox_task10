using Microsoft.EntityFrameworkCore;
using Task10.Data;

namespace Task10.Data;

public class InMemoryAppContext: BaseApplicationContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
    }
}