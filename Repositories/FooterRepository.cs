using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

using System.Threading.Tasks;
using Dapper;
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

        // Get all Footers
        public async Task<List<Footer>> GetFooters()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Footers";
            var footers = await connection.QueryAsync<Footer>(query);
            return footers.AsList();
        }

        // Get a single Footer by ID
        public async Task<Footer> GetFooterById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Footers WHERE ID = @Id";
            var footer = await connection.QueryFirstOrDefaultAsync<Footer>(query, new { Id = id });

            if (footer == null)
            {
                throw new KeyNotFoundException($"Footer with ID {id} not found.");
            }

            return footer;
        }

        // Create a new Footer
        public async Task<Footer> CreateFooter(Footer footer, Task<int> task)
        {
            if (footer == null)
            {
                throw new ArgumentNullException(nameof(footer), "Footer cannot be null.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO Footers (Content, CreatedDate)
                VALUES (@Content, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            Task<int> task1 = connection.QuerySingleAsync<int>(query, new
            {
                footer.Content,
                CreatedDate = DateTime.UtcNow
            });
            var id = await task1;
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
                UPDATE Footers
                SET Content = @Content,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
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
            var query = "DELETE FROM Footers WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Footer with ID {id} not found.");
            }
        }

        internal async Task CreateFooter(Footer footer, object task)
        {
            throw new NotImplementedException();
        }
    }
}
