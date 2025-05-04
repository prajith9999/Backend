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
    public class SubscriptionDetailsRepository : ISubscriptionDetailsRepository
    {
        private readonly SqlConnection _connection;

        public SubscriptionDetailsRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<SubscriptionDetails>> GetAll()
        {
            var query = "SELECT * FROM SubscriptionDetailss";
            var result = await _connection.QueryAsync<SubscriptionDetails>(query);
            return (List<SubscriptionDetails>)result;
        }

        public async Task<SubscriptionDetails> GetById(int id)
        {
            var query = "SELECT * FROM SubscriptionDetailss WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<SubscriptionDetails>(query, new { id });
            return result;
        }

        public async Task<SubscriptionDetails> Create(SubscriptionDetails item)
        {
            var query = "INSERT INTO SubscriptionDetailss (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<SubscriptionDetails> Update(int id, SubscriptionDetails item)
        {
            var query = "UPDATE SubscriptionDetailss SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM SubscriptionDetailss WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
