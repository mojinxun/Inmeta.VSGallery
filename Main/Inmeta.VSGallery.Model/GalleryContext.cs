using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Inmeta.VSGallery.Model
{
    public class GalleryContext : DbContext
    {
        public GalleryContext()
            : base("InmetaGallery")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<Extension>()
                .HasOptional<Release>(u => u.Release)
                .WithOptionalDependent(c => c.Extension).Map(p => p.MapKey("ReleaseID"));
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<ReleaseRating> ReleaseRatings { get; set; }
        public DbSet<Extension> Extensions { get; set; }

        public IQueryable<Release> ReleasesWithStuff
        {
            get
            {
                return Releases.Include(e => e.Extension).Include(e => e.Project).Include(e => e.Ratings);
            }
        }

        public IQueryable<Extension> ExtensionsWithStuff
        {
            get
            {
                return Extensions.Include(e => e.Release.Project);
            }
        }

        public Extension GetExtensionByVsixId(string vsixId)
        {
            return Extensions.Include(e => e.Release).FirstOrDefault(e => e.VsixId == vsixId);
        }

        public void DeleteExtension(string vsixId)
        {
            var extension = GetExtensionByVsixId(vsixId);
            Projects.Remove(extension.Release.Project);
            ReleaseRatings.RemoveRange(extension.Release.Ratings);
            Releases.Remove(extension.Release);
            Extensions.Remove(extension);
            SaveChanges();
        }
    }
}
