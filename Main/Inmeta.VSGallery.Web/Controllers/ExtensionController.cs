using System;
using System.Linq;
using System.Web.Mvc;
using Inmeta.VSGallery.Model;
using Inmeta.VSGallery.Web.Models;

namespace Inmeta.VSGallery.Web.Controllers
{
    public partial class ExtensionController : Controller
    {
        public virtual ActionResult Index(string vsixId)
        {
            using (var ctx = new GalleryContext())
            {
                var e = ctx.ExtensionsWithStuff.First(ex => ex.VsixId == vsixId);
                if (Request.Url == null)
                {
                    throw new ArgumentException("Could not get Request Url");    
                }

                return View(new ExtensionViewModel(e, e.Release.Project.Description, e.Release.GetAverageRating(), Request.Url.ToString()));                
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Delete")]
        public virtual ActionResult Delete(FormCollection data, string vsixId)
        {
            using (var ctx = new GalleryContext())
            {
                ctx.DeleteExtension(vsixId);
            }
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Update")]
        public virtual ActionResult Post(FormCollection data, string vsixId)
        {
            var rating = GetRating(data);

            using (var ctx = new GalleryContext())
            {
                var extension = ctx.ExtensionsWithStuff.First(e => e.VsixId == vsixId);
                extension.AddRating(rating);

                ctx.SaveChanges();
            }
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        private static int GetRating(FormCollection data)
        {
            int rating = 0;
            foreach (var key in data.AllKeys)
            {
                int keyId;
                if( Int32.TryParse(key, out keyId))
                {
                    var selectedRating = data.GetValue(key);
                    rating = Convert.ToInt32(selectedRating.AttemptedValue);
                }
            }
            return rating;
        }
    }
}