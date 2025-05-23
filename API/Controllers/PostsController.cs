using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[] { "Post 1", "Post 2", "Post 3" });
        }

        [HttpPost]
        public IActionResult Create(string title)
        {
            return Ok($"Created: {title}");
        }
    }
}
