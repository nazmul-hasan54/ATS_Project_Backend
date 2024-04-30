using AtsProjectWithAngular.Domain.Context;
using AtsProjectWithAngular.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtsProjectWithAngular.DTO;

namespace AtsProjectWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMapper _mapper;
        public UsersController(BlogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("get-all-user")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var users = await _dbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return Ok(user);
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(User users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Users.Add(users);
                    await _dbContext.SaveChangesAsync();
                    return CreatedAtAction("GetUserById", new { id=users.UserId}, users);
                }
                else
                {
                    return BadRequest("Please pass all the value");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user-login")]
        public async Task<ActionResult<UserLoginDTO>> UserLogin(User user)
        {
            var userExists = _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
            if (userExists == null)
            {
                return BadRequest();
            }
            var userMap = new UserLoginDTO
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            return Ok(userMap);
        }

        //public string GetUserByName(string userName) 
        //{
        //    var user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);
        //    return user.ToString();
        //}
    }
}

