using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using LandWind.Models;

namespace LandWind.Repositories
{
    public interface IFaqRepository
    {
        Task<List<Faq>> GetAll(); // Get all FAQs
        Task<Faq> GetFaqById(int id); // Get a FAQ by ID
        Task<Faq> CreateFaq(Faq item); // Create a new FAQ
        Task<Faq> UpdateFaq(int id, Faq faq); // Update a FAQ
        Task<bool> DeleteFaq(int id); // Delete a FAQ
    }

    public class FaqRepository : IFaqRepository
    {
        private readonly IDbConnection _connection;

        public FaqRepository(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<List<Faq>> GetAll()
        {
            var query = "SELECT * FROM Faq";
            var result = await _connection.QueryAsync<Faq>(query);
            return result.AsList(); // Convert to List<Faq>
        }

        public async Task<Faq> GetFaqById(int id)
        {
            var query = "SELECT * FROM Faq WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Faq>(query, new { id });
            return result;
        }

        public async Task<Faq> CreateFaq(Faq item)
        {
            var query = "INSERT INTO Faq (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id; // Set the generated ID
            return item;
        }

        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            var query = "UPDATE Faq SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { faq.Title, faq.ModifiedBy, faq.ModifiedDate, id });
            return faq;
        }

        public async Task<bool> DeleteFaq(int id)
        {
            var query = "DELETE FROM Faq WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
