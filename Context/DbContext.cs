using Microsoft.EntityFrameworkCore;
using UserNotepad.Models;

namespace UserNotepad.Context{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options): base(options){}
        public DbSet<User> Users {get; set;} = null!;
        public DbSet<ExtraData> ExtraDatas {get; set;} = null!;
    }
}