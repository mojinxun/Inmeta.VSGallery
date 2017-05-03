using System;
using System.Collections.Generic;
using System.Linq;
using Inmeta.VSGallery.Model;

namespace Inmeta.VSGallery.Web.Models
{
    public class ReleasesViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<ReleaseViewModel> Releases { get; set; }

        [Obsolete("Only used by MVC model binder. Don't use!!!!")]
        public ReleasesViewModel()
        {

        }
        public ReleasesViewModel(IEnumerable<ReleaseViewModel> releases)
        {
            Releases = releases;
        }

        public ReleasesViewModel(IEnumerable<Release> releases)
        {
            Releases = releases.Select(e => new ReleaseViewModel(e));
        }
    }

    public class ReleaseViewModel
    {
        public ReleaseViewModel()
        {

        }

        public ReleaseViewModel(Release e)
        {
            AverageRating = e.GetAverageRating();
            Icon = e.Extension.Icon;
            NrDownloads = e.DownloadCount;
            Title = e.Extension.Name;
            Description = e.Extension.Description;
            Author = e.Extension.Author;
            ModifiedDate = e.Project.ModifiedDate;
            VsixId = e.Extension.VsixId;
            StarRating = new StarRating(e.Id, AverageRating);
            NrRatings = e.Ratings.Count();
        }

        public string VsixId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Icon { get; set; }
        public int NrDownloads { get; set; }
        public double AverageRating { get; set; }
        public int NrRatings { get; set; }

        public StarRating StarRating { get; set; }

    }
}