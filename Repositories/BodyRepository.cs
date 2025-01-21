using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class BodyRepository
    {
        private readonly string _connectionString;

        // Constructor to inject connection string
        public BodyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to fetch all Body records
        public async Task<List<Body>> GetBodies()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(); // Ensure the connection is open
            var query = "SELECT * FROM Bodies";
            var bodies = await connection.QueryAsync<Body>(query);
            return bodies.AsList();
        }

        // Method to fetch a single Body record by ID
        public async Task<Body> GetBodyById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(); // Ensure the connection is open
            var query = "SELECT * FROM Bodies WHERE Id = @Id";
            var body = await connection.QueryFirstOrDefaultAsync<Body>(query, new { Id = id });

            if (body == null)
            {
                throw new KeyNotFoundException($"Body with ID {id} not found.");
            }

            return body;
        }

        // Method to insert a new Body record and return the created body
        public async Task<Body> InsertBody(Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            // Set CreatedDate if not set
            body.CreatedDate ??= DateTime.UtcNow;

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(); // Ensure the connection is open
            var query = @"
                INSERT INTO Bodies (Title, TitleDescription, OrderNumber, CreatedBy, CreatedDate)
                VALUES (@Title, @TitleDescription, @OrderNumber, @CreatedBy, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.QuerySingleAsync<int>(query, body);
            body.ID = id;  // Use the primary key for the ID field
            return body; // Return the created body with its ID populated
        }

        // Method to update an existing Body record
        public async Task<Body> UpdateBody(int id, Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(); // Ensure the connection is open
            var query = "SELECT * FROM Bodies WHERE Id = @Id";
            var existingBody = await connection.QueryFirstOrDefaultAsync<Body>(query, new { Id = id });

            if (existingBody == null)
            {
                throw new KeyNotFoundException($"Body with ID {id} not found.");
            }

            // Update query
            var updateQuery = @"
                UPDATE Bodies 
                SET Title = @Title, 
                    TitleDescription = @TitleDescription, 
                    OrderNumber = @OrderNumber, 
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate 
                WHERE Id = @Id";

            await connection.ExecuteAsync(updateQuery, new
            {
                body.Title,
                body.TitleDescription,
                body.OrderNumber,
                body.ModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                Id = id
            });

            return body; // Return the updated record
        }

        // Method to delete a Body record by ID
        public async Task<bool> DeleteBody(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(); // Ensure the connection is open
            var query = "DELETE FROM Bodies WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0; // Return true if deletion was successful, otherwise false
        }
    }
}
