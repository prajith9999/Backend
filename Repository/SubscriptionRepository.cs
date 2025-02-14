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
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetAll();
        Task<Subscription> GetById(int id);
        Task<Subscription> Create(Subscription item);
        Task<Subscription> Update(int id, Subscription item);
        Task<bool> Delete(int id);
        //Task<object> GetSubscription();
        Task<bool> DeleteSubscription(int id);
        Task<object> UpdateSubscription(int id, Subscription subscription);
        Task<object> InsertSubscription(Subscription subscription);
        Task<object> GetSubscriptionById(int id);
    }
}

namespace LandWind.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IDbConnection _connection;

        public SubscriptionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Subscription>> GetAll()
        {
            var query = "SELECT * FROM Subscription";
            var result = await _connection.QueryAsync<Subscription>(query);
            return (List<Subscription>)result;
        }

        public async Task<Subscription> GetById(int id)
        {
            var query = "SELECT * FROM Subscription WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Subscription>(query, new { id });
            return result;
        }

        public async Task<Subscription> Create(Subscription item)
        {
            var query = "INSERT INTO Subscription (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<Subscription> Update(int id, Subscription item)
        {
            var query = "UPDATE Subscription SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Subscription WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }

        //public Task<object> GetSubscription()
        //{
        //    throw new NotImplementedException();
        //}

        public Task<bool> DeleteSubscription(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateSubscription(int id, Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<object> InsertSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSubscriptionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}


//public async Task<List<Subscription>> GetAll()
//{
//    var query = "SELECT * FROM Subscription";  // This will fetch SubscriptionName, Features, and Price
//    var result = await _connection.QueryAsync<Subscription>(query);
//    return (List<Subscription>)result;
//}

//public async Task<Subscription> GetById(int id)
//{
//    // Updated query to fetch Features and Price along with Title and ID
//    var query = "SELECT ID, Title, Features, Price FROM Subscription WHERE ID = @id";
//    var result = await _connection.QueryFirstOrDefaultAsync<Subscription>(query, new { id });
//    return result;
//}


//public Task<object> GetSubscription()
//{
//    throw new NotImplementedException();
