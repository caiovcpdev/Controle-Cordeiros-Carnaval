using ControleCordeirosCarnaval.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleCordeirosCarnaval.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> opt) : base (opt) 
        {  
        }
        public DbSet<CordeiroModel> cordeiro { get; set; }
    }
}
