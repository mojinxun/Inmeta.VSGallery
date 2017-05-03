using System;

namespace Inmeta.VSGallery.Web.Models
{
    public class StarRating
    {
        public int ReleaseId { get; set; }
        public int NrStars { get; set; }

        public StarRating(int releaseId, double averageRating)
        {
            ReleaseId = releaseId;
            NrStars = Convert.ToInt32(averageRating);
        }
    }
}