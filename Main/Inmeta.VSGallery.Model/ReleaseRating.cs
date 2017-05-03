using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmeta.VSGallery.Model
{
    public class ReleaseRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rating { get; set; }

        public Release Release { get; set; }

        public ReleaseRating()
        {

        }

        public ReleaseRating(Release e, int rating)
        {
            Release = e;
            Rating = rating;
        }
    }
}