using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LandWind.Models;
using System.ComponentModel.DataAnnotations;

namespace LandWind.Repositories
{
    // Interface for FeaturePageRepository
    public interface IFeaturePageRepository
    {
        Task<List<FeaturePage>> GetAll();
        Task<FeaturePage> GetById(int id);
        Task<FeaturePage> Create(FeaturePage featurePage);
        Task<FeaturePage> Update(int id, FeaturePage featurePage);
        Task<bool> Delete(int id);
    }

    // Repository class implementing IFeaturePageRepository
    public class FeaturePageRepository : IFeaturePageRepository
    {
        private readonly IDbConnection _connection;

        // Constructor to inject the connection string (or use DI for connection)
        public FeaturePageRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Get all feature pages
        public async Task<List<FeaturePage>> GetAll()
        {
            var result = await _connection.QueryAsync<FeaturePage>("SELECT * FROM FeaturePage");
            return result.AsList();
        }

        // Get feature page by ID
        public async Task<FeaturePage> GetById(int id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<FeaturePage>(
                "SELECT * FROM FeaturePage WHERE Id = @Id", new { Id = id });
            return result;
        }

        // Create a new feature page
        public async Task<FeaturePage> Create(FeaturePage featurePage)
        {
            var query = "INSERT INTO FeaturePage (Name, Description) VALUES (@Name, @Description); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.QuerySingleAsync<int>(query, featurePage);
            featurePage.ID = id; // Update the ID field of the featurePage object
            return featurePage;
        }

        // Update an existing feature page
        public async Task<FeaturePage> Update(int id, FeaturePage featurePage)
        {
            var query = "UPDATE FeaturePage SET Name = @Name, Description = @Description WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { featurePage.Name, featurePage.Description, Id = id });
            return featurePage;
        }

        // Delete a feature page by ID
        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM FeaturePage WHERE Id = @Id";
            var affectedRows = await _connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;
        }
    }
}
