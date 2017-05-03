using System.Collections.Generic;
using System.IO;
using System.Linq;
using VSIX.v20;
using VSIXParser;

namespace Inmeta.VSIX
{
    public class VsixItem
    {
        public string VsixId { get; set; }
        public string VsixVersion { get; set; }
        public string ManifestVersion { get; set; }
        public string Publisher { get; set; }
        public string Type { get; set; }

        public byte[] Content { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MoreInfo { get; set; }

        public string Icon { get; set; }
        public byte[] IconContent { get; set; }

        public string PreviewImage { get; set; }

        public byte[] PreviewImageContent { get; set; }

        public VsixItem(Vsix v, byte[] content)
        {
            this.ManifestVersion = v.Version;
            this.VsixId = v.Identifier.Id;
            this.VsixVersion = v.Identifier.Version;
            this.Publisher = v.Identifier.Author;
            this.DisplayName = v.Identifier.Name;
            this.Description = v.Identifier.Description;
            this.Icon = v.Identifier.Icon;
            this.PreviewImage = v.Identifier.PreviewImage;
            this.MoreInfo = v.Identifier.MoreInfoUrl;
            this.Content = content;

            this.IconContent = this.GetResourceContent(this.Icon);
            this.PreviewImageContent = this.GetResourceContent(this.PreviewImage);
        }

        private byte[] GetResourceContent(string name)
        {
            if (name == null) 
                return null;

            var image = this.GetVsixResource(name);
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }            
        }

        public VsixItem(PackageManifest p, byte[] content)
        {
            this.ManifestVersion = p.Version;
            this.VsixId = p.Metadata.Identity.Id;
            this.VsixVersion = p.Metadata.Identity.Version;
            this.Publisher = p.Metadata.Identity.Publisher;
            this.DisplayName = p.Metadata.DisplayName;
            this.Description = p.Metadata.Description;
            this.Icon = p.Metadata.Icon;
            this.PreviewImage = p.Metadata.PreviewImage;
            this.MoreInfo = p.Metadata.MoreInfo;
            this.Content = content;

            this.IconContent = this.GetResourceContent(this.Icon);
            this.PreviewImageContent = this.GetResourceContent(this.PreviewImage);

        }

        private Stream GetVsixResource(string name)
        {
            name = Path.GetFileName(name);
            var sr = new MemoryStream(Content);
            var file = new VsixFile(sr, DisplayName, FileAccess.Read);

            var root = file.Nodes[0];
            return TraverseNodes(root.Nodes, name);
        }

        private Stream TraverseNodes(IEnumerable<VsixFileNode> nodes, string name)
        {
            foreach (var n in nodes)
            {
                if (n.Name.ToLower() == name.ToLower())
                {
                    var part = n as VsixPackagePartNode;
                    return part.Part.GetStream(FileMode.Open);
                }
                if (n.Nodes.Any())
                {
                    var image = TraverseNodes(n.Nodes, name);
                    if (image != null)
                        return image;
                }

            }
            return null;
        }

    }
}