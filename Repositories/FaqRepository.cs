using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FaqRepository
    {
        private readonly string _connectionString;

        // Constructor to inject connection string
        public FaqRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null.");
        }

        // Get all FAQs
        public async Task<List<Faq>> GetFaqs()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Faqs";
            var faqs = await connection.QueryAsync<Faq>(query);
            return faqs?.AsList() ?? new List<Faq>(); // Ensure an empty list if no results are found
        }

        // Get FAQ by ID
        public async Task<Faq> GetFaqById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Faqs WHERE ID = @Id";
            var faq = await connection.QueryFirstOrDefaultAsync<Faq>(query, new { Id = id });

            if (faq == null)
            {
                // Handle null FAQ gracefully
                return null; // Or throw an exception if you need this to fail
            }

            return faq;
        }

        // Create a new FAQ
        public async Task<Faq> CreateFaq(Faq faq)
        {
            if (faq == null)
            {
                return null; // Handle null FAQ gracefully, you could also throw an exception here if desired
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO Faqs (Question, Answer, CreatedDate)
                VALUES (@Question, @Answer, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                faq.Question,
                faq.Answer,
                CreatedDate = DateTime.UtcNow
            });

            faq.ID = id; // Set the ID of the FAQ object to the newly generated ID
            return faq;
        }

        // Update an FAQ
        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            if (faq == null)
            {
                return null; // If FAQ is null, return null (or throw exception depending on desired behavior)
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE Faqs
                SET Question = @Question,
                    Answer = @Answer,
                    ModifiedDate = @ModifiedDate
                WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                faq.Question,
                faq.Answer,
                ModifiedDate = DateTime.UtcNow,
                Id = id
            });

            if (rowsAffected == 0)
            {
                return null; // No record updated, return null
            }

            return faq;
        }

        // Delete an FAQ
        public async Task<bool> DeleteFaq(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Faqs WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0; // Return true if deletion was successful
        }
    }
}
