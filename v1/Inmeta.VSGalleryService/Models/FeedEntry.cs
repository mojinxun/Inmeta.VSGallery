using System;
using System.IO;
using Inmeta.VSGalleryService.Controllers;

namespace Inmeta.VSGalleryService.Models
{
    public class FeedEntry
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string MoreInfo { get; set; }
        public string Summary { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public FeedEntryAuthor Author { get; set; }
        public string Category { get; set; }
        public FeedEntryContent Content { get; set; }
        public FeedEntryVsix Vsix { get; set; }

        public FeedEntry(string f, string category)
        {
            Updated = new FileInfo(f).LastWriteTime;
            Published = new FileInfo(f).CreationTime;
            Category = category;
        }
    }
}