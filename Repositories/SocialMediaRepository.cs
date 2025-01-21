using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class SocialMediaRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public SocialMediaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all Social Media records
        public async Task<List<SocialMedia>> GetSocialMediaAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SocialMedia";
            var result = await connection.QueryAsync<SocialMedia>(query);
            return result.AsList();
        }

        // Get Social Media by ID
        public async Task<SocialMedia> GetSocialMediaByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SocialMedia WHERE ID = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<SocialMedia>(query, new { Id = id });
            return result;
        }

        // Create new Social Media record
        public async Task<SocialMedia> CreateSocialMediaAsync(SocialMedia socialMedia)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO SocialMedia (Name, URL, CreatedDate)
                VALUES (@Name, @URL, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                socialMedia.Name,
                socialMedia.URL,
                CreatedDate = DateTime.UtcNow
            });

            socialMedia.ID = id;
            return socialMedia;
        }

        // Update existing Social Media record
        public async Task<SocialMedia> UpdateSocialMediaAsync(int id, SocialMedia socialMedia)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE SocialMedia
                SET Name = @Name,
                    URL = @URL,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id;
                SELECT * FROM SocialMedia WHERE ID = @Id";
            var updatedSocialMedia = await connection.QueryFirstOrDefaultAsync<SocialMedia>(query, new
            {
                id,
                socialMedia.Name,
                socialMedia.URL,
                ModifiedDate = DateTime.UtcNow
            });

            return updatedSocialMedia;
        }

        // Delete a Social Media record
        public async Task<bool> DeleteSocialMediaAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM SocialMedia WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
