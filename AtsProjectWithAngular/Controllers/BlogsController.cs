using AtsProjectWithAngular.Domain;
using AtsProjectWithAngular.Domain.Context;
using AtsProjectWithAngular.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtsProjectWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDbContext _blogDbContext;
        private IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public BlogsController(BlogDbContext blogDbContext, 
            IWebHostEnvironment environment, 
            IMapper mapper)
        {
            _blogDbContext = blogDbContext;
            _environment = environment;
            _mapper = mapper;
        }
        [HttpGet("get-all-blogs")]
        public async Task<ActionResult> GetAllBlog()
        {
            var blog = await _blogDbContext.Blogs.ToListAsync();
            var result = _mapper.Map<IEnumerable<BlogDTO>>(blog);
            return Ok(result);
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogDbContext.Blogs.FindAsync(id);
            var result = _mapper.Map<BlogDTO>(blog);
            return Ok(result);
        }
        [HttpPost("add-blog")]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filePath = SaveImage(blog.FormFile);

                    BlogDTO newBlog = new BlogDTO
                    {
                        BlogId = blog.BlogId,
                        BlogTitle = blog.BlogTitle,
                        BlogDescription = blog.BlogDescription,
                        CategoryId = blog.CategoryId,
                        CreatedDate = blog.CreatedDate,
                        BlogImage = filePath
                    };
                    var result = _mapper.Map<Blog>(newBlog);
                    _blogDbContext.Blogs.Add(result);
                    await _blogDbContext.SaveChangesAsync();
                    return CreatedAtAction("GetBlogById", new { id= blog.BlogId }, blog);
                }
                else
                {
                    return BadRequest("Please pass the all value");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("update-blog/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, Blog blog) 
        {
            if (id != blog.BlogId) 
            {
                return BadRequest();
            }
            var result = _mapper.Map<BlogDTO>(blog);
            _blogDbContext.Entry(result).State = EntityState.Modified;
            try
            {
                await _blogDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }
        private string SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extentions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtension = new string[] { ".jpg", ".png", ".jpeg", ".webp" };
                if (!allowedExtension.Contains(ext))
                {
                    string msg = string.Format("Only {0} extions are allowed", string.Join(",", allowedExtension));
                    return msg;
                }
                string uniqueString = Guid.NewGuid().ToString();
                // We are trying to create new file name here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return newFileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string GetImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extentions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtension = new string[] { ".jpg", ".png", ".jpeg", ".webp" };
                if (!allowedExtension.Contains(ext))
                {
                    string msg = string.Format("Only {0} extions are allowed", string.Join(",", allowedExtension));
                    return msg;
                }
                string uniqueString = Guid.NewGuid().ToString();
                // We are trying to create new file name here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return newFileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
