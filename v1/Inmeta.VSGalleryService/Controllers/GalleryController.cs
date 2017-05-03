using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Inmeta.VSGalleryService.Models;
using Inmeta.VSGalleryService.Properties;
using VSIX.v20;
using VSIXParser;

namespace Inmeta.VSGalleryService.Controllers
{
    public class GalleryController : ApiController
    {
        /// <summary>
        /// Returns a list of all extensions found as feed entries
        /// </summary>
        public IEnumerable<FeedEntry> Get()
        {
            var entries = new List<FeedEntry>();

            var rootPath = Settings.Default.VSIXAbsolutePath;
            ValidateRootPath(rootPath);
            CreateFeedEntries(entries, rootPath);
            return entries;
        }

        /// <summary>
        /// Returns the extension with a given name
        /// </summary>
        /// <param name="name"></param>
        [HttpGet]
        public HttpResponseMessage Extension(string name)
        {
            return Extension(null, name);
        }

        /// <summary>
        /// Returns the extension with the given name in the given category (e.g. folder)
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Extension(string category, string name)
        {
            string relativePath = String.IsNullOrEmpty(category) ? name : category + "\\" + name;
            string extensionPath = Path.Combine(Settings.Default.VSIXAbsolutePath, relativePath);

            if( !File.Exists(extensionPath))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(extensionPath, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vsix");

            return result;
        }

        /// <summary>
        /// Crawls directory recursively for VSIX files. Uses the folder names as category
        /// </summary>
        private void CreateFeedEntries(List<FeedEntry> entries, string rootPath)
        {
            try
            {
                foreach( var dir in Directory.EnumerateDirectories(rootPath))
                {
                    CreateFeedEntries(entries,dir);
                }

                string category = Path.GetFileName(rootPath);
                var files = Directory.EnumerateFiles(rootPath, "*.vsix");
                foreach( var f in files)
                {
                    using (var file = new VsixFile(f, FileAccess.Read))
                    {
                        var root = file.Nodes[0];

                        var manifestNode = root.Nodes.FirstOrDefault(n => n.Name.EndsWith(".vsixmanifest"));
                        var part = manifestNode as VsixPackagePartNode;

                        FeedEntry entry;
                        try
                        {
                            entry = DeserializeManifest(part, f, category);
                        }
                        catch (InvalidOperationException)
                        {
                            entry = DeserializeManifestV2(part, f, category);
                        }
                        entries.Add(entry);
                    }
                }
            }
            catch (Exception s)
            {
                Console.WriteLine(s);
            }
        }

        private FeedEntry DeserializeManifestV2(VsixPackagePartNode part, string fileName, string category)
        {
            var entry = new FeedEntry(fileName, category);

            using (var stream = part.Part.GetStream(FileMode.Open))
            {
                using (var sr = new StreamReader(stream))
                {
                    var manifest = PackageManifest.Deserialize(sr.ReadToEnd());

                    entry.Id = manifest.Metadata.Identity.Id;
                    entry.Title = manifest.Metadata.DisplayName;
                    entry.Author = new FeedEntryAuthor {Name = manifest.Metadata.Identity.Publisher};
                    entry.Content = new FeedEntryContent {Src = BuildUri(category, fileName), Type = "octet/stream"};
                    entry.Vsix = new FeedEntryVsix
                        {
                            Id = manifest.Metadata.Identity.Id, 
                            Version = manifest.Metadata.Identity.Version
                        };
                    entry.Summary = manifest.Metadata.Description;
                    entry.MoreInfo = manifest.Metadata.MoreInfo;
                }
            }
            return entry;
        }

        private FeedEntry DeserializeManifest(VsixPackagePartNode part, string fileName, string category)
        {
            var entry = new FeedEntry(fileName, category);
            using (var stream = part.Part.GetStream(FileMode.Open))
            {
                var vsixSerializer = new VsixSerializer();
                var vsix = vsixSerializer.Deserialize(stream) as Vsix;
                if( vsix == null)
                    throw new Exception("Could not deserialize VSIX manifest from " + fileName);
                entry.Title = vsix.Identifier.Name;
                entry.Id = vsix.Identifier.Id;
                entry.Author = new FeedEntryAuthor {Name = vsix.Identifier.Author};
                entry.Content = new FeedEntryContent {Src = BuildUri(category,fileName), Type = "octet/stream"};
                entry.Vsix = new FeedEntryVsix {Id = vsix.Identifier.Id, Version = vsix.Identifier.Version};
                entry.MoreInfo = vsix.References.Any() ? vsix.References.First().MoreInfoUrl : string.Empty;
                entry.Summary = vsix.Identifier.Description;
            }
            return entry;
        }

        private string BuildUri(string category, string extensionFileName)
        {
            string baseUrl = HttpContext.Current.Request.Url.ToString();
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";

            var extensionName = Path.GetFileName(extensionFileName);
            if (!String.IsNullOrEmpty(category))
                return string.Format("{0}gallery/Extension/{1}/{2}", baseUrl, category, extensionName);
            return string.Format("{0}gallery/Extension/{1}", baseUrl, extensionName);
        }

        private static void ValidateRootPath(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Could not locate path: " + rootPath),
                    ReasonPhrase = "Critical Exception"
                });

            }
        }
    }
}