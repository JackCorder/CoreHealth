using Microsoft.EntityFrameworkCore;

namespace EcommerceRESTGen6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
