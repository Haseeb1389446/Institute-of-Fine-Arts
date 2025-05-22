using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Institute_Of_Fine_Arts.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }

        public DbSet<Award> Awards { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ExhibitedPainting> ExhibitedPaintings { get; set; }

    }
}
