using Microsoft.AspNetCore.Mvc;

namespace UserManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Managers.UserManager _userManager;

        public UserController(Managers.UserManager userManager)
        {
            _userManager = userManager;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var response = _userManager.GetUserById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromQuery]string email, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            var userId = _userManager.AddUser(email, firstName, lastName);

            if (userId == null)
            {
                return Conflict();
            }

            return Ok(userId);
        }
    }
}
