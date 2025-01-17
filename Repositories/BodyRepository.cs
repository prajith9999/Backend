using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vueproject_asp.Data;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class BodyRepository
    {
        private readonly AppDbContext _context;

        public BodyRepository(AppDbContext context)
        {
            _context = context;
        }

        // Method to fetch all Body records
        public async Task<List<Body>> GetBodies()
        {
           
            return await _context.Bodies.ToListAsync();
        }

        // Method to fetch a single Body record by ID
        public async Task<Body> GetBodyById(int id)
        {
            return await _context.Bodies.FindAsync(id);  // Use FindAsync for primary key lookups
        }

        // Method to insert a new Body record and return the created body
        public async Task<Body> InsertBody(Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            body.CreatedDate ??= DateTime.UtcNow;  // Set CreatedDate if not set

            await _context.Bodies.AddAsync(body);
            await _context.SaveChangesAsync();
            return body;  // Return the created body with its ID populated
        }

        // Method to update an existing Body record
        public async Task<Body> UpdateBody(int id, Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            var existingBody = await _context.Bodies.FindAsync(id);  // Using FindAsync for primary key lookup

            if (existingBody == null)
            {
                throw new KeyNotFoundException("Body with the specified ID not found.");
            }

            existingBody.Title = body.Title;
            existingBody.TitleDescription = body.TitleDescription;
            existingBody.OrderNumber = body.OrderNumber;
            existingBody.ModifiedBy = body.ModifiedBy;
            existingBody.ModifiedDate = DateTime.UtcNow;  // Optionally set the ModifiedDate

            await _context.SaveChangesAsync();
            return existingBody;  // Return the updated record
        }

        // Method to delete a Body record by ID
        public async Task<bool> DeleteBody(int id)
        {
            var body = await _context.Bodies.FromSqlRaw("EXEC GetBodyById @Id = {0}", id).FirstOrDefaultAsync();// Using FindAsync for primary key lookup

            if (body == null)
            {
                return false;  // Return false if body not found
            }

            _context.Bodies.Remove(body);
            await _context.SaveChangesAsync();
            return true;  // Return true if deletion was successful
        }
    }
}
