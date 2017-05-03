using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Inmeta.VSGallery.Web.Controllers
{
    using System.Web;

    public class DownloadImageResponseMessage : HttpResponseMessage
    {
        public DownloadImageResponseMessage(byte[] imageContent, string name)
        {
            StatusCode = HttpStatusCode.OK;
            Content = new ByteArrayContent(imageContent);
            Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(name));
        }
    }
}