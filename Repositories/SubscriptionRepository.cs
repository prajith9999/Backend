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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SqlConnection _connection;

        public SubscriptionRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Subscription>> GetAll()
        {
            var query = "SELECT * FROM Subscriptions";
            var result = await _connection.QueryAsync<Subscription>(query);
            return (List<Subscription>)result;
        }

        public async Task<Subscription> GetById(int id)
        {
            var query = "SELECT * FROM Subscriptions WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Subscription>(query, new { id });
            return result;
        }

        public async Task<Subscription> Create(Subscription item)
        {
            var query = "INSERT INTO Subscriptions (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<Subscription> Update(int id, Subscription item)
        {
            var query = "UPDATE Subscriptions SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Subscriptions WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
