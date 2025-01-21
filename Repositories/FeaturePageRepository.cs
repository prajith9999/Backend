using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FeaturePageRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public FeaturePageRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null.");
        }

        // Get all FeaturePages
        public async Task<List<FeaturePage>> GetFeaturePages()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM FeaturePage"; // Ensure the table name matches your database
            var featurePages = await connection.QueryAsync<FeaturePage>(query);
            return featurePages.AsList();
        }

        // Get a single FeaturePage by ID
        public async Task<FeaturePage> GetFeaturePageById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM FeaturePage WHERE ID = @Id"; // Ensure the table name matches your database
            var featurePage = await connection.QueryFirstOrDefaultAsync<FeaturePage>(query, new { Id = id });

            if (featurePage == null)
            {
                throw new KeyNotFoundException($"FeaturePage with ID {id} not found.");
            }

            return featurePage;
        }

        // Create a new FeaturePage
        public async Task<FeaturePage> CreateFeaturePage(FeaturePage featurePage)
        {
            if (featurePage == null)
            {
                throw new ArgumentNullException(nameof(featurePage), "FeaturePage cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO FeaturePage (Title, HeadingDescription, OrderNumber, CreatedDate, Description)
                VALUES (@Title, @HeadingDescription, @OrderNumber, @CreatedDate, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int)"; // Ensure the table name matches your database
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                featurePage.Title,
                featurePage.HeadingDescription,
                featurePage.OrderNumber,
                CreatedDate = DateTime.UtcNow,
                featurePage.Description
            });

            featurePage.ID = id;
            return featurePage;
        }

        // Update an existing FeaturePage
        public async Task UpdateFeaturePage(FeaturePage featurePage)
        {
            if (featurePage == null)
            {
                throw new ArgumentNullException(nameof(featurePage), "FeaturePage cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE FeaturePage
                SET Title = @Title,
                    HeadingDescription = @HeadingDescription,
                    OrderNumber = @OrderNumber,
                    Description = @Description,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id"; // Ensure the table name matches your database
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                featurePage.Title,
                featurePage.HeadingDescription,
                featurePage.OrderNumber,
                featurePage.Description,
                ModifiedDate = DateTime.UtcNow,
                Id = featurePage.ID
            });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"FeaturePage with ID {featurePage.ID} not found.");
            }
        }

        // Delete a FeaturePage
        public async Task<bool> DeleteFeaturePage(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM FeaturePage WHERE ID = @Id"; // Ensure the table name matches your database
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0; // Return true if deletion was successful
        }
    }
}
