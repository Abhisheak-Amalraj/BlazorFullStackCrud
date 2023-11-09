using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterIntakeController : ControllerBase
    {
        private readonly DataContext _context;

        public WaterIntakeController(DataContext context)
        {

            _context = context;
        }


        // GET endpoint to retrieve all water intake records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterIntake>>> GetWaterIntakeRecords()
        {
            try
            {
                // Includes the User navigation property and retrieves all records asynchronously
                var waterIntakeRecords = await _context.WaterIntakes.Include(wi => wi.User).ToListAsync();
                return Ok(waterIntakeRecords);
            }
            catch (Exception ex)
            {
                // Returns a generic HTTP 500 (Internal Server Error) response
                return StatusCode(500, "An internal server error has occurred.");
            }
        }


        /*
         '.Include(wi => wi.User).FirstOrDefaultAsync(wi => wi.ID == id)' is part of 
         an Entity Framework Core (EF Core) query that interacts with a SQL database

        .Include(wi => wi.User): This is used to specify that the query should 
        not only retrieve the WaterIntake entity but also include the related User entity

        .FirstOrDefaultAsync(wi => wi.ID == id): This method asynchronously retrieves the 
        first WaterIntake entity that matches the condition (wi.ID == id) or returns null 
        if no such entity exists.
         */

        // GET endpoint to retrieve a single water intake record by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<WaterIntake>> GetWaterIntakeRecord(int id)
        {
            try
            {
                // Attempts to find the record with the given ID
                var waterIntakeRecord = await _context.WaterIntakes
                    .Include(wi => wi.User)
                    .FirstOrDefaultAsync(wi => wi.ID == id);

                // If the record is not found, returns HTTP 404 (Not Found)
                if (waterIntakeRecord == null)
                {
                    return NotFound("Water Intake record not found.");
                }

                // If found, returns the record with an HTTP 200 (OK) status
                return Ok(waterIntakeRecord);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // POST endpoint to create a new water intake record
        [HttpPost]
        public async Task<ActionResult<IEnumerable<WaterIntake>>> CreateWaterIntakeRecord(WaterIntake waterIntake)
        {
            // Adds the new water intake record to the database context
            _context.WaterIntakes.Add(waterIntake);

            await _context.SaveChangesAsync();

            // Returns the updated list of all water intake records
            return (await _context.WaterIntakes.ToListAsync());
        }


        // PUT endpoint to update an existing water intake record by ID
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<WaterIntake>>> UpdateWaterIntakeRecord(int id, WaterIntake waterIntake)
        {
            // Checks for an ID mismatch
            if (id != waterIntake.ID)
            {
                return BadRequest("ID mismatch.");
            }

            // Finds the existing record in the database
            var existingRecord = await _context.WaterIntakes.FindAsync(id);

            // If not found, returns HTTP 404 (Not Found)
            if (existingRecord == null)
            {
                return NotFound("Water Intake record not found.");
            }

            // Updates the properties of the existing record
            existingRecord.UserID = waterIntake.UserID;
            existingRecord.IntakeDate = waterIntake.IntakeDate;
            existingRecord.ConsumedWater = waterIntake.ConsumedWater;

            // Saves the changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Returns the updated list of all water intake records
            return Ok(await _context.WaterIntakes.ToListAsync());
        }


        // DELETE endpoint to delete a water intake record by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<WaterIntake>>> DeleteWaterIntakeRecord(int id)
        {
            //Attempts to find the WaterIntake record with the specified ID in the database
            var waterIntakeRecord = await _context.WaterIntakes.FindAsync(id);

            // If no record is found with that ID, the method returns a 404 Not Found response
            if (waterIntakeRecord == null)
            {
                return NotFound("Water Intake record not found.");
            }

            // If the record is found, it's removed from the database context
            _context.WaterIntakes.Remove(waterIntakeRecord);
            await _context.SaveChangesAsync();

            //Returns a 200 OK response, along with the updated list of all WaterIntake records
            return Ok(await _context.WaterIntakes.ToListAsync());
        }
    }
}
