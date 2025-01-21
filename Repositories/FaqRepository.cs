using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FaqRepository
    {
        private readonly string _connectionString;

        public FaqRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null.");
        }

        // Get all FAQs
        public async Task<List<Faq>> GetFaqs()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.faq"; // Updated to match the table name
            var faqs = await connection.QueryAsync<Faq>(query);
            return faqs.AsList();
        }

        // Get FAQ by ID
        public async Task<Faq> GetFaqById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.faq WHERE ID = @Id"; // Updated to match the table name
            return await connection.QueryFirstOrDefaultAsync<Faq>(query, new { Id = id });
        }

        // Create a new FAQ
        public async Task<Faq> CreateFaq(Faq faq)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO dbo.faq (Title, Question, Answer, CreatedDate) 
                VALUES (@Title, @Question, @Answer, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)"; // Updated to match the table name
            faq.ID = await connection.QuerySingleAsync<int>(query, new
            {
                faq.Title,
                faq.Question,
                faq.Answer,
                CreatedDate = DateTime.UtcNow
            });
            return faq;
        }

        // Update an FAQ
        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE dbo.faq
                SET Title = @Title, Question = @Question, Answer = @Answer, ModifiedDate = @ModifiedDate
                WHERE ID = @Id"; // Updated to match the table name
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                faq.Title,
                faq.Question,
                faq.Answer,
                ModifiedDate = DateTime.UtcNow,
                Id = id
            });
            return rowsAffected > 0 ? faq : null;
        }

        // Delete an FAQ
        public async Task<bool> DeleteFaq(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM dbo.faq WHERE ID = @Id"; // Updated to match the table name
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
