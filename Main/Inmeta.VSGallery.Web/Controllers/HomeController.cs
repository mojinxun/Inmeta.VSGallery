using System;
using System.Linq;
using System.Web.Mvc;
using Inmeta.VSGallery.Model;
using Inmeta.VSGallery.Web.Models;

namespace Inmeta.VSGallery.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            using (var ctx = new GalleryContext())
            {
                var extensions = ctx.ReleasesWithStuff.OrderByDescending(r => r.DownloadCount).Take(10);
                var model = new ReleasesViewModel(extensions.ToList());
                return View(model);
            }            
        }

        [HttpPost]
        public virtual ActionResult Search(ReleasesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(model.SearchText))
                    return RedirectToAction(MVC.Home.ActionNames.Index);

                using (var ctx = new GalleryContext())
                {
                    ReleasesViewModel m = model;
                    var extensions = ctx.ReleasesWithStuff.Where(r => r.Extension.Name.Contains(m.SearchText) || r.Extension.Description.Contains(m.SearchText));
                    model = new ReleasesViewModel(extensions.ToList());
                }
            }
            return View(Views.Index, model);
        }


    }
}