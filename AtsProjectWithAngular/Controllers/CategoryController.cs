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
    public class CategoryController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory() 
        {
            var category = await _context.Categories.ToListAsync();
            var result = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            return Ok(result);
        }
    }
}
