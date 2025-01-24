using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;

namespace LandWind.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        private readonly SqlConnection _connection;

        public FaqRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Faq>> GetAll()
        {
            var query = "SELECT * FROM Faqs";
            var result = await _connection.QueryAsync<Faq>(query);
            return (List<Faq>)result;
        }

        public async Task<Faq> GetById(int id)
        {
            var query = "SELECT * FROM Faqs WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Faq>(query, new { id });
            return result;
        }

        public async Task<Faq> Create(Faq item)
        {
            var query = "INSERT INTO Faqs (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<Faq> Update(int id, Faq item)
        {
            var query = "UPDATE Faqs SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Faqs WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
