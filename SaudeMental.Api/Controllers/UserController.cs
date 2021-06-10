using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaudeMental.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace SaudeMental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<UserController>/5
        [HttpGet("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var token = await _userService.GetTokenAsync(userName);
            if (string.IsNullOrEmpty(token))
                return NotFound();
            return Ok(token);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var userName = await _userService.RegisterAsync();
            if (string.IsNullOrEmpty(userName))
                return BadRequest();
            var routeValues = new { userName = userName };
            return CreatedAtAction(nameof(Get), routeValues, userName);
        }

        // POST api/<UserController>/5
        [HttpPost("{userName}")]
        public async Task<IActionResult> Post(string userName)
        {
            var user = await _userService.RegisterAsync(userName);
            if (string.IsNullOrEmpty(user))
                return BadRequest();
            var routeValues = new { userName = user };
            return CreatedAtAction(nameof(Get), routeValues, user);
        }

        [Authorize]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            var result = await _userService.DeleteAsync(userName);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
