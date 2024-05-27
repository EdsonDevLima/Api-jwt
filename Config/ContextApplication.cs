using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Config{
    public class ContextApplication:DbContext{
        public ContextApplication(DbContextOptions options):base(options){}

        public DbSet<Users> Users{get;set;}
    }
}