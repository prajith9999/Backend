using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class SubscriptionRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public SubscriptionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all subscriptions
        public async Task<List<Subscription>> GetSubscription()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Subscription";  // Simple SELECT query
            var subscriptions = await connection.QueryAsync<Subscription>(query);
            return subscriptions.AsList();
        }

        // Get a subscription by ID
        public async Task<Subscription> GetSubscriptionById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Subscription WHERE ID = @Id";  // Query by ID
            var subscription = await connection.QueryFirstOrDefaultAsync<Subscription>(query, new { Id = id });
            return subscription;  // Return the subscription or null if not found
        }

        // Insert a new subscription
        public async Task InsertSubscription(Subscription subscription)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO dbo.Subscription (Name, StartDate, EndDate, Price)
                VALUES (@Name, @StartDate, @EndDate, @Price);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";  // Insert and return the ID
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                subscription.Name,
                subscription.StartDate,
                subscription.EndDate,
                subscription.Price
            });

            subscription.ID = id;  // Set the ID of the inserted subscription
        }

        // Update an existing subscription
        public async Task UpdateSubscription(Subscription subscription)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE dbo.Subscription
                SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate, Price = @Price
                WHERE ID = @Id";  // Update query
            await connection.ExecuteAsync(query, new
            {
                subscription.ID,
                subscription.Name,
                subscription.StartDate,
                subscription.EndDate,
                subscription.Price
            });
        }

        // Delete a subscription
        public async Task<bool> DeleteSubscription(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM dbo.Subscription WHERE ID = @Id";  // Delete query
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;  // Return true if rows are affected
        }
    }
}
