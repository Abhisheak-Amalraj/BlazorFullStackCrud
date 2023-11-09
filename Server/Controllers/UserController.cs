using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Private field to interact with the database context
        private readonly DataContext _context;

        // Constructor that accepts a DataContext instance 
        public UserController(DataContext context)
        {
            _context = context;
        }


        // Action method to retrieve all users from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Asynchronously gets the list of users from the database and returns it with an HTTP 200 (OK) status
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }


        // Action method to retrieve a single user based on their ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            // Asynchronously finds the user with the provided ID or returns null if not found
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);

            // If the user isn't found, returns an HTTP 404 (Not Found) status
            if (user == null)
            {
                return NotFound("Sorry, user not found!");
            }
            // If the user is found, returns it with an HTTP 200 (OK) status
            return Ok(user);
        }


        // Action method to create a new user
        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser(User user)
        {
            // Checks if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Adds the new user to the database context
            _context.Users.Add(user);

            // Saves the changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Returns the updated list of users with an HTTP 200 (OK) status
            return Ok(await GetDbUsers());
        }


        // Action method to update an existing user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            // Checks if the ID in the URL matches the ID of the user provided in the request body
            if (id != user.ID)
            {
                return BadRequest();
            }

            // Marks the user entity as modified in the database context
            _context.Entry(user).State = EntityState.Modified;

            // Iterates over the associated water intake records to update them or add new ones
            foreach (var waterIntake in user.WaterIntakes)
            {
                if (_context.WaterIntakes.Any(wi => wi.ID == waterIntake.ID))
                {
                    _context.Entry(waterIntake).State = EntityState.Modified;
                }
                else
                {
                    _context.WaterIntakes.Add(waterIntake);
                }
            }

            // Tries to save the changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }

            // Catches any concurrency exceptions that may occur if the user was updated concurrently
            catch (DbUpdateConcurrencyException)
            {
                // Checks if the user still exists. If not, returns HTTP 404 (Not Found)
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Returns an HTTP 204 (No Content) status if the update was successful
            return NoContent();
        }


        // Helper method to check if a user exists in the database by their ID
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }


        // Action method to delete a user by their ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            // Finds the user in the database; if not found, returns HTTP 404 (Not Found)
            var dbUser = await _context.Users.FirstOrDefaultAsync(sh => sh.ID == id);
            if (dbUser == null)
                return NotFound("Sorry, but no User");

            // Removes the user from the database context
            _context.Users.Remove(dbUser);

            // Saves the changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Returns the updated list of users with an HTTP 200 (OK) status
            return Ok(await GetDbUsers());
        }


        // Helper method to get the list of users from the database
        private async Task<List<User>> GetDbUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
