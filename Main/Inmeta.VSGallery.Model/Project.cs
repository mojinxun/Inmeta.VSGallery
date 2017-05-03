using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmeta.VSGallery.Model
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Project()
        {

        }
        public Project(string title, string description)
        {
            Title = title;
            Description = description;
            ModifiedDate = DateTime.Now;
        }

    }
}