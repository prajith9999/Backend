using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FooterRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public FooterRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all Footer
        public async Task<List<Footer>> GetFooter()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Footer"; // Using dbo.Footer
            var footer = await connection.QueryAsync<Footer>(query);
            return footer.AsList();
        }

        // Get a single Footer by ID
        public async Task<Footer> GetFooterById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Footer WHERE ID = @Id"; // Using dbo.Footer
            var footer = await connection.QueryFirstOrDefaultAsync<Footer>(query, new { Id = id });

            if (footer == null)
            {
                throw new KeyNotFoundException($"Footer with ID {id} not found.");
            }

            return footer;
        }

        // Create a new Footer
        public async Task<Footer> CreateFooter(Footer footer)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO dbo.Footer (PageId, FooterTitle, FooterDescription, Content, CreatedDate)
                VALUES (@PageId, @FooterTitle, @FooterDescription, @Content, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)"; // Using dbo.Footer
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                footer.PageId,
                footer.FooterTitle,
                footer.FooterDescription,
                footer.Content,
                CreatedDate = DateTime.UtcNow
            });

            footer.ID = id;
            return footer;
        }

        // Update an existing Footer
        public async Task UpdateFooter(Footer footer)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE dbo.Footer
                SET PageId = @PageId,
                    FooterTitle = @FooterTitle,
                    FooterDescription = @FooterDescription,
                    Content = @Content,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id"; // Using dbo.Footer
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                footer.PageId,
                footer.FooterTitle,
                footer.FooterDescription,
                footer.Content,
                ModifiedDate = DateTime.UtcNow,
                Id = footer.ID
            });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Footer with ID {footer.ID} not found.");
            }
        }

        // Delete a Footer
        public async Task DeleteFooter(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM dbo.Footer WHERE ID = @Id"; // Using dbo.Footer
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Footer with ID {id} not found.");
            }
        }
    }
}
