using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;
using System.Data;



namespace LandWind.Interfaces
{
    public interface ISubscriptionDetailsRepository
    {
        Task<List<SubscriptionDetails>> GetAll();
        Task<SubscriptionDetails> GetById(int id);
        Task<SubscriptionDetails> Create(SubscriptionDetails item);
        Task<SubscriptionDetails> Update(int id, SubscriptionDetails item);
        Task<bool> Delete(int id);
        //Task GetSubscriptionDetails();
        //Task GetSubscriptionDetailById(int id);
        //Task<bool> DeleteSubscriptionDetail(int id, string deletedBy);
        //Task CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail);
        //Task UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail);
    }
}

namespace LandWind.Repositories
{
    public class SubscriptionDetailsRepository : ISubscriptionDetailsRepository
    {
        private readonly IDbConnection _connection;

        public SubscriptionDetailsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<SubscriptionDetails>> GetAll()
        {
            var query = "SELECT * FROM SubscriptionDetails";
            var result = await _connection.QueryAsync<SubscriptionDetails>(query);
            return (List<SubscriptionDetails>)result;
        }

        public async Task<SubscriptionDetails> GetById(int id)
        {
            var query = "SELECT * FROM SubscriptionDetails WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<SubscriptionDetails>(query, new { id });
            return result;
        }

        public async Task<SubscriptionDetails> Create(SubscriptionDetails item)
        {
            var query = "INSERT INTO SubscriptionDetails (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<SubscriptionDetails> Update(int id, SubscriptionDetails item)
        {
            var query = "UPDATE SubscriptionDetails SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM SubscriptionDetails WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }

        //public Task GetSubscriptionDetails()
        //{
        //    throw new NotImplementedException();
        //}

        public Task GetSubscriptionDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSubscriptionDetail(int id, string deletedBy)
        {
            throw new NotImplementedException();
        }

        public Task CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            throw new NotImplementedException();
        }

        //public Task UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
