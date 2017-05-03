using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Inmeta.VSGalleryService.Models;

namespace Inmeta.VSGalleryService.Formatters
{
    public class SyndicationFeedFormatter : MediaTypeFormatter
    {
        private const string Atom = "application/atom+xml";
        private const string Rss = "application/rss+xml";
        private const string VsixNamespace = "http://schemas.microsoft.com/developer/vsx-syndication-schema/2010";

        public SyndicationFeedFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Atom));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Rss));
        }

        private readonly Func<Type, bool> supportedType = type => (type == typeof (IEnumerable<FeedEntry>));

        public override bool CanReadType(Type type)
        {
            return supportedType(type);
        }

        public override bool CanWriteType(Type type)
        {
            return supportedType(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream,
                                                System.Net.Http.HttpContent content,
                                                System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
                {
                    if (type == typeof (FeedEntry) || type == typeof (IEnumerable<FeedEntry>))
                        BuildSyndicationFeed(value, writeStream, content.Headers.ContentType.MediaType);
                });
        }

        private void BuildSyndicationFeed(object models, Stream stream, string contenttype)
        {
            var items = new List<SyndicationItem>();
            var feed = new SyndicationFeed
                {
                    Title = new TextSyndicationContent(Properties.Settings.Default.FeedTitle)
                };

            if (models is IEnumerable<FeedEntry>)
            {
                var enumerator = ((IEnumerable<FeedEntry>) models).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var entry = BuildSyndicationItem(enumerator.Current);
                    items.Add(entry);
                }
            }
            else
            {
                items.Add(BuildSyndicationItem((FeedEntry) models));
            }

            feed.Items = items;

            using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings {Indent = true}))
            {
                if (string.Equals(contenttype, Atom))
                {
                    var atomformatter = new Atom10FeedFormatter(feed);
                    atomformatter.WriteTo(writer);
                }
                else
                {
                    var rssformatter = new Rss20FeedFormatter(feed);
                    rssformatter.WriteTo(writer);
                }
            }
        }

        private SyndicationItem BuildSyndicationItem(FeedEntry u)
        {
            var item = new SyndicationItem
                {
                    Title = new TextSyndicationContent(u.Title),
                    BaseUri = null,
                    LastUpdatedTime = u.Updated,
                    Content = new UrlSyndicationContent(new Uri(u.Content.Src, UriKind.Absolute), "octet/stream")
                };

            item.Categories.Add(new SyndicationCategory(u.Category));
            item.Summary = new TextSyndicationContent(u.Summary);
            item.Authors.Add(new SyndicationPerson {Name = u.Author.Name});
            item.PublishDate = u.Published;
            if (!string.IsNullOrEmpty(u.MoreInfo))
            {
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(u.MoreInfo), "More Information"));
            }
            item.ElementExtensions.Add(new XElement(XName.Get("Vsix", VsixNamespace),
                                                    new XElement(XName.Get("Id", VsixNamespace), u.Vsix.Id),
                                                    new XElement(XName.Get("Version", VsixNamespace), u.Vsix.Version)).
                                           CreateReader());
            return item;
        }
    }
}