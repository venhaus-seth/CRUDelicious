#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CRUDelicious.Models; // fill in PrjectName
public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
    // create the following line for every model
    public DbSet<Dish> Dishs { get; set; } 
}