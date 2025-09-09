using chit_chat_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;



namespace chit_chat_api.DB_Data
{
    public class _dbContext : DbContext
    {
        public _dbContext(DbContextOptions<_dbContext> options) : base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Profile_Image> ProfileImages { get; set; }
    }
}
