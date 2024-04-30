using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AtsProjectWithAngular.DTO
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogDescription { get; set; }
        public int CategoryId { get; set; }
        public string? BlogImage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
