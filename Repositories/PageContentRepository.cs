using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class PageContentRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public PageContentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all PageContents
        public async Task<List<PageContent>> GetPageContents()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM PageContent";
            var pageContent = await connection.QueryAsync<PageContent>(query);
            return pageContent.AsList();
        }

        // Get a single PageContent by ID
        public async Task<PageContent> GetPageContentById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM PageContents WHERE ID = @Id";
            return await connection.QueryFirstOrDefaultAsync<PageContent>(query, new { Id = id });
        }

        // Create a new PageContent
        public async Task<PageContent> CreatePageContent(PageContent pageContent)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO PageContents (Title, HighLights, OrderNumber, Content, CreatedDate)
                VALUES (@Title, @HighLights, @OrderNumber, @Content, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                pageContent.Title,
                pageContent.HighLights,
                pageContent.OrderNumber,
                pageContent.Content,
                CreatedDate = DateTime.UtcNow
            });

            pageContent.ID = id;
            return pageContent;
        }

        // Update an existing PageContent
        public async Task UpdatePageContent(PageContent pageContent)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE PageContents
                SET Title = @Title,
                    HighLights = @HighLights,
                    OrderNumber = @OrderNumber,
                    Content = @Content,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                pageContent.Title,
                pageContent.HighLights,
                pageContent.OrderNumber,
                pageContent.Content,
                ModifiedDate = DateTime.UtcNow,
                Id = pageContent.ID
            });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"PageContent with ID {pageContent.ID} not found.");
            }
        }

        // Delete a PageContent
        public async Task<bool> DeletePageContent(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM PageContents WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }

        internal async Task<PageContent> InsertPageContent(PageContent pageContent)
        {
            throw new NotImplementedException();
        }
    }
}
