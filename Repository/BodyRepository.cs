using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Repositories
{
    // Interface for the BodyRepository
    public interface IBodyRepository
    {
        Task<List<Body>> GetAll(); // Fetch all bodies
        Task<Body> GetById(int id); // Fetch a body by its ID
        Task<Body> Create(Body item); // Create a new body
        Task<Body> Update(int id, Body item); // Update an existing body
        Task<bool> Delete(int id); // Delete a body by its ID
        Task<List<Body>> GetBodies(); // Fetch all bodies (another version)
        Task<Body> GetBodyById(int id); // Fetch a body by its ID (another version)
        Task<Body> InsertBody(Body body); // Insert a new body
        Task<Body> UpdateBody(int id, Body body); // Update a body by its ID (another version)
        Task<bool> DeleteBody(int id); // Delete a body by its ID (another version)
        Task<object?> GetAllBodies(); // Fetch all bodies as an object
    }

    // Implementation of the BodyRepository
    public class BodyRepository : IBodyRepository
    {
        private readonly IDbConnection _connection;

        public BodyRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Fetch all bodies from the database
        public async Task<List<Body>> GetAll()
        {
            var query = "SELECT * FROM Bodies";
            var result = await _connection.QueryAsync<Body>(query);
            return result.AsList();
        }

        // Fetch a body by its ID
        public async Task<Body> GetById(int id)
        {
            var query = "SELECT * FROM Bodies WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Body>(query, new { id });
            return result;
        }

        // Create a new body in the database
        public async Task<Body> Create(Body item)
        {
            var query = "INSERT INTO Bodies (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        // Update an existing body in the database
        public async Task<Body> Update(int id, Body item)
        {
            var query = "UPDATE Bodies SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        // Delete a body from the database
        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Bodies WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }

        // Fetch all bodies (another version)
        public async Task<List<Body>> GetBodies()
        {
            return await GetAll(); // Use GetAll to fetch bodies
        }

        // Fetch a body by its ID (another version)
        public async Task<Body> GetBodyById(int id)
        {
            return await GetById(id); // Use GetById to fetch body by ID
        }

        // Insert a new body
        public async Task<Body> InsertBody(Body body)
        {
            return await Create(body); // Use Create method for insertion
        }

        // Update a body by its ID (another version)
        public async Task<Body> UpdateBody(int id, Body body)
        {
            return await Update(id, body); // Use Update method for updating
        }

        // Delete a body by its ID (another version)
        public async Task<bool> DeleteBody(int id)
        {
            return await Delete(id); // Use Delete method for deletion
        }

        // Fetch all bodies as an object (another version)
        public async Task<object?> GetAllBodies()
        {
            return await GetAll(); // Use GetAll to fetch bodies as an object
        }
    }
}
