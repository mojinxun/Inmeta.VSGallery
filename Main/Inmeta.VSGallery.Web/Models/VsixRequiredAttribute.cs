using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Inmeta.VSGallery.Web.Models
{
    public class VsixRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var file = value as HttpPostedFileBase;
                if (file != null && !file.FileName.ToLower().EndsWith(".vsix"))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}