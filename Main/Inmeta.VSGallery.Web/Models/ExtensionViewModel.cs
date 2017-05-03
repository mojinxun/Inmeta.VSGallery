using Inmeta.VSGallery.Model;

namespace Inmeta.VSGallery.Web.Models
{
    public class ExtensionViewModel
    {
        public Extension Extension { get; set; }
        public string ProjectDescription { get; set; }
        public StarRating StarRating { get; set; }
        public string DownloadUrl { get; set; }
        public ExtensionViewModel(Extension e, string projectDescription, double averageRating, string baseUrl)
        {
            Extension = e;
            ProjectDescription = projectDescription;
            StarRating = new StarRating(e.Release.Id, averageRating);
            DownloadUrl = e.DownloadUrl(baseUrl);
        }
    }
}