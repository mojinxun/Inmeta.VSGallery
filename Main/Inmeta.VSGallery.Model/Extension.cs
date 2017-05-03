using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inmeta.VSIX;

namespace Inmeta.VSGallery.Model
{
    public class Extension
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VsixId { get; set; }
        public string VsixVersion { get; set; }
        public string Tool { get; set; }
        public string Author { get; set; }
        public string PreviewImage { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MoreInfo { get; set; }
        public virtual byte[] Content { get; set; }

        public virtual byte[] IconContent { get; set; }
        public virtual byte[] PreviewImageContent { get; set; }
        public Release Release { get; set; }
        public string ManifestVersion { get; set; }

        public string DownloadUrl(string baseUrl)
        {
            return baseUrl + "/api/Download?vsixid=" + VsixId;
        }

        public Extension()
        {
        }
        public Extension(VsixItem item, byte[] vsix)
        {
            Update(item, vsix);
        }

        public Dictionary<string, string> VsixMetadata(string baseUrl)
        {
                return new Dictionary<string, string>
                {
                    {"VsixId", VsixId},
                    {"VsixVersion", VsixVersion},
                    {"Type", "Tool"},
                    {"DownloadUrl", DownloadUrl(baseUrl) },
                    {"DownloadUpdateUrl", DownloadUrl(baseUrl)},
                    {"Author", Author},
                    {"PreviewImage", baseUrl + "/api/PreviewImage?vsixId=" + VsixId},
                    {"Icon", baseUrl + "/api/Icon?vsixId=" + VsixId }
                };
        }

        public void Update(VsixItem item, byte[] vsix)
        {
            Author = item.Publisher;
            Content = vsix;
            Icon = item.Icon;
            IconContent = item.IconContent;
            Name = item.DisplayName;
            PreviewImage = item.PreviewImage;
            PreviewImageContent = item.PreviewImageContent;
            Tool = item.Type;
            VsixId = item.VsixId;
            VsixVersion = item.VsixVersion;
            Description = item.Description;
            ManifestVersion = item.ManifestVersion;
            Description = item.Description;
            MoreInfo = item.MoreInfo;
        }

        public void AddRating(int rating)
        {
            if (rating != 0)
            {
                Release.Ratings.Add(new ReleaseRating(Release, rating));
            }
        }

        public void IncreaseDownloadCount()
        {
            Release.DownloadCount += 1;
        }
    }
}