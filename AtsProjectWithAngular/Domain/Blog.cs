using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AtsProjectWithAngular.Domain
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogDescription { get; set; }
        public int CategoryId { get; set; }
        public string? BlogImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Category? Category { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
