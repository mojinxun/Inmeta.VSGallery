using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inmeta.VSGallery.Web.Controllers
{
    using Model;

    public class IconController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string vsixId)
        {
            using (var ctx = new GalleryContext())
            {
                var ex = ctx.GetExtensionByVsixId(vsixId);
                if (ex == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

                return new DownloadImageResponseMessage(ex.IconContent, ex.Icon);
            }
        }
    }
        public class PreviewImageController : ApiController
        {
        [HttpGet]
        public HttpResponseMessage Get(string vsixId)
        {
            using (var ctx = new GalleryContext())
            {
                var ex = ctx.GetExtensionByVsixId(vsixId);
                if (ex == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                return new DownloadImageResponseMessage(ex.PreviewImageContent, ex.PreviewImage);
            }
        }

    }
}
