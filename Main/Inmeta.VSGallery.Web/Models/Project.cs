namespace Galleries.Domain.Model
{
    public partial class Project
    {
        public Project()
        {

        }

        public Project(Inmeta.VSGallery.Model.Project p)
        {
            Description = p.Description;
            Title = p.Title;
            ModifiedDate = p.ModifiedDate;
            FileReleaseEnabled = true;
            IsPublished = true;
        }
    }
}