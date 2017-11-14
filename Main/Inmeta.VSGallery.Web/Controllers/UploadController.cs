using System;
using System.Linq;
using System.Web.Mvc;
using Inmeta.VSGallery.Model;
using Inmeta.VSGallery.Web.Models;
using Inmeta.VSIX;

namespace Inmeta.VSGallery.Web.Controllers
{
    public partial class UploadController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Post(UploadExtensionModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new GalleryContext())
                {
                    var vsix = model.ReadFileContent();
                    var vsixItem = VsixRepository.Read(vsix, model.File.FileName);
                    var extension = ctx.ExtensionsWithStuff.FirstOrDefault(e => e.VsixId == vsixItem.VsixId);
                    if (extension == null)
                    {
                        extension = new Extension(vsixItem, vsix);
                        var project = new Project(extension.Name, extension.Description);
                        var release = new Release(project, extension);
                        ctx.Releases.Add(release);
                    }
                    else
                    {
                        extension.Update(vsixItem, vsix);
                        extension.Release.Project.ModifiedDate = DateTime.Now;
                    }

                    ctx.SaveChanges();
                    return RedirectToAction(MVC.Extension.Index(extension.VsixId));
                }
            }
            return View(Views.Index);
        }
    }
}