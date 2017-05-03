using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inmeta.VSGallery.Model
{
    public class Release
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ICollection<ReleaseRating> Ratings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public int DownloadCount { get; set; }

        public virtual Project Project { get; set; }

        public virtual Extension Extension { get; set; }

        internal void AddRating(int rating)
        {
            Ratings.Add(new ReleaseRating(this, rating));
        }

        public Release()
        {
            Ratings = new List<ReleaseRating>();
        }

        public Release(Project project, Extension newExtension)
        {
            Project = project;
            Extension = newExtension;
            DownloadCount = 0;
        }

        public double GetAverageRating()
        {
            if (!Ratings.Any())
                return 0;

            return Ratings.Average(r => r.Rating);
        }
    }
}