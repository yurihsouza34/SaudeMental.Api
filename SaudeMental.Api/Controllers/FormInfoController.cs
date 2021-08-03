using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaudeMental.Api.Context;
using SaudeMental.Api.Model;
using SaudeMental.Api.Services;

namespace SaudeMental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormInfoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public FormInfoController(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/FormInfo/User/5
        [HttpGet("User/{userId}/N/{num}")]
        public async Task<ActionResult<List<FormInfo>>> GetFormInfo(string userId, int num)
        {
            var formInfoCount = await _context.FormInfos.Where(u => u.UserId == userId).CountAsync();

            if (formInfoCount == 0)
                return NotFound();

            var formInfo = await _context.FormInfos.Where(u => u.UserId == userId).Skip(Math.Max(0, formInfoCount - num)).ToListAsync();

            if (formInfo == null || formInfo.Count == 0)
            {
                return NotFound();
            }

            return formInfo;
        }

        // GET: api/FormInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormInfo>> GetFormInfo(string id)
        {
            var formInfo = await _context.FormInfos.FindAsync(id);

            if (formInfo == null)
            {
                return NotFound();
            }

            return formInfo;
        }

        // POST: api/FormInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("User/{userId}/")]
        public async Task<ActionResult<FormInfo>> PostFormInfo([FromBody]FormInfo formInfo, string userId)
        {
            //var userId = await _userService.GetIdAsync(User.Identity.Name);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            formInfo.UserId = userId;
            _context.FormInfos.Add(formInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FormInfoExists(formInfo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFormInfo", new { id = formInfo.Id }, formInfo);
        }

        // DELETE: api/FormInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormInfo(string id)
        {
            var formInfo = await _context.FormInfos.FindAsync(id);
            if (formInfo == null)
            {
                return NotFound();
            }

            _context.FormInfos.Remove(formInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormInfoExists(string id)
        {
            return _context.FormInfos.Any(e => e.Id == id);
        }
    }
}
