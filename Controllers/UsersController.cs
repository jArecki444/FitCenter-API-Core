using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       private readonly DataContext _context;
       public UsersController(DataContext context)
       {
           _context = context;

       }

        // GET api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET api/Users/5
       [AllowAnonymous]
       [HttpGet("{id}")]
       public async Task<IActionResult> GetValue(int id)
       {
           var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
           return Ok(user);
   }


        // POST api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
