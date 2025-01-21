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
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }

            _connectionString = connectionString; // Initialize the connection string
        }

        // Get all subscriptions
        public async Task<List<Subscription>> GetSubscriptions()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.GetSubscriptions";  // Execute stored procedure
            var subscriptions = await connection.QueryAsync<Subscription>(query);
            return subscriptions.AsList();
        }

        // Get a subscription by ID
        public async Task<Subscription> GetSubscriptionById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.GetSubscriptionById @Id";  // Execute stored procedure with parameters
            var subscription = await connection.QueryFirstOrDefaultAsync<Subscription>(query, new { Id = id });
            return subscription;  // Return the subscription or null if not found
        }

        // Insert a new subscription
        public async Task InsertSubscription(Subscription subscription)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                EXEC dbo.CreateSubscription 
                    @Name = @Name, 
                    @StartDate = @StartDate, 
                    @EndDate = @EndDate, 
                    @Price = @Price"; // Call the stored procedure
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
                EXEC dbo.UpdateSubscription 
                    @Id = @Id, 
                    @Name = @Name, 
                    @StartDate = @StartDate, 
                    @EndDate = @EndDate,
                    @Price = @Price"; // Call the stored procedure
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
            var query = "EXEC dbo.DeleteSubscription @Id";  // Call the stored procedure
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;  // Return true if rows are affected
        }
    }
}
