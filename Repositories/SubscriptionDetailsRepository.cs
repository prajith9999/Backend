using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

using System.Threading.Tasks;
using Dapper;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class SubscriptionDetailsRepository
    {
        private readonly string _connectionString;

        // Constructor to inject the connection string
        public SubscriptionDetailsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all subscription details
        public async Task<List<SubscriptionDetails>> GetSubscriptionDetails()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SubscriptionDetails";
            var subscriptionDetails = await connection.QueryAsync<SubscriptionDetails>(query);
            return subscriptionDetails.AsList();
        }

        // Get a subscription detail by ID
        public async Task<SubscriptionDetails> GetSubscriptionDetailById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SubscriptionDetails WHERE ID = @Id";
            return await connection.QueryFirstOrDefaultAsync<SubscriptionDetails>(query, new { Id = id });
        }

        // Create a new subscription detail
        public async Task<SubscriptionDetails> CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO SubscriptionDetails (SubscriptionID, Detail, Value)
                VALUES (@SubscriptionID, @Detail, @Value);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                subscriptionDetail.SubscriptionID,
                subscriptionDetail.Detail,
                subscriptionDetail.Value
            });

            subscriptionDetail.ID = id;
            return subscriptionDetail;
        }

        // Update an existing subscription detail
        public async Task<SubscriptionDetails> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE SubscriptionDetails
                SET SubscriptionID = @SubscriptionID,
                    Detail = @Detail,
                    Value = @Value
                WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                subscriptionDetail.SubscriptionID,
                subscriptionDetail.Detail,
                subscriptionDetail.Value,
                Id = id
            });

            if (rowsAffected == 0)
            {
                return null; // No record was updated
            }

            return subscriptionDetail;
        }

        // Delete a subscription detail
        public async Task<bool> DeleteSubscriptionDetail(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM SubscriptionDetails WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
