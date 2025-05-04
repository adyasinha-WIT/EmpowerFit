using Microsoft.EntityFrameworkCore;


namespace ExFit.DataAcces.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



    }
}
