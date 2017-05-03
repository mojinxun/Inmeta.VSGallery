using System.Web.Mvc;
using Inmeta.VSGallery.Web.Controllers;
using Inmeta.VSGallery.Web.Models;
using Inmeta.VSGallery.Web.Test.Properties;
using Inmeta.VSIX;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Inmeta.VSGallery.Web.Test
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        [Ignore]
        public void IndexTest()
        {
            var controller = new HomeController();
            var view = controller.Index();
            Assert.IsTrue(view is ViewResult);
            Assert.IsTrue((view as ViewResult).Model is ReleasesViewModel);
        }

    }
}
