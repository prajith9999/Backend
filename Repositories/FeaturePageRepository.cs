using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;  // Add Dapper to the imports
using LandWind.Models;
using LandWind.Interfaces;
using System.Data.SqlClient;
using LandWind.Repositories;// Assuming you're using SQL Server

namespace LandWind.Repositories
{
    public class FeaturePageRepository : IFeaturePageRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string (or use DI for connection)
        public FeaturePageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection DbConnection => new SqlConnection(_connectionString);

        // Get all feature pages
        public async Task<List<FeaturePage>> GetAll()
        {
            using (var connection = DbConnection)
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<FeaturePage>("SELECT * FROM FeaturePages");
                return result.AsList();
            }
        }

        // Get feature page by ID
        public async Task<FeaturePage> GetById(int id)
        {
            using (var connection = DbConnection)
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<FeaturePage>(
                    "SELECT * FROM FeaturePages WHERE Id = @Id", new { Id = id });
                return result;
            }
        }

        // Create a new feature page
        public async Task<FeaturePage> Create(FeaturePage featurePage)
        {
            using (var connection = DbConnection)
            {
                await connection.OpenAsync();
                var query = "INSERT INTO FeaturePages (Name, Description) VALUES (@Name, @Description); SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var id = await connection.QuerySingleAsync<int>(query, featurePage);
                featurePage.Id = id; // Assuming FeaturePage has Id as a property
                return featurePage;
            }
        }

        // Update an existing feature page
        public async Task<FeaturePage> Update(int id, FeaturePage featurePage)
        {
            using (var connection = DbConnection)
            {
                await connection.OpenAsync();
                var query = "UPDATE FeaturePages SET Name = @Name, Description = @Description WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { featurePage.Name, featurePage.Description, Id = id });
                return featurePage;
            }
        }

        // Delete a feature page by ID
        public async Task<bool> Delete(int id)
        {
            using (var connection = DbConnection)
            {
                await connection.OpenAsync();
                var query = "DELETE FROM FeaturePages WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}
