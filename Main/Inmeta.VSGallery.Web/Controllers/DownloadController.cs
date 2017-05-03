using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inmeta.VSGallery.Model;

namespace Inmeta.VSGallery.Web.Controllers
{
    public class DownloadController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string vsixId)
        {
            using (var ctx = new GalleryContext())
            {
                var extension = ctx.GetExtensionByVsixId(vsixId);
                if (extension == null) 
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                extension.IncreaseDownloadCount();
                ctx.SaveChanges();
                return new DownloadExtensionResponseMessage(extension);
            }
        }

    }
}