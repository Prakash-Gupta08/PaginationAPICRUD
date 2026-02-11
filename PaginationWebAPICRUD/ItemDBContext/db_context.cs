using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaginationWebAPICRUD.Data;
using PaginationWebAPICRUD.ItemDBContext;


namespace PaginationWebAPICRUD.ItemDBContext
{
    public class db_context : DbContext
    {
        private readonly IConfiguration _configuration;
        internal object gadetgadet;


    public db_context(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<gadet> gadet { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var data_string = _configuration.GetConnectionString("MySqlConn");

        optionsBuilder.UseSqlServer(data_string);
    }



    }
}