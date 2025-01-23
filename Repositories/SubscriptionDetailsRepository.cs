using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var query = "SELECT * FROM SubscriptionDetails WHERE DeletedDate IS NULL"; // Only non-deleted records
            var subscriptionDetails = await connection.QueryAsync<SubscriptionDetails>(query);
            return subscriptionDetails.AsList();
        }

        // Get a subscription detail by ID
        public async Task<SubscriptionDetails> GetSubscriptionDetailById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM SubscriptionDetails WHERE DetailID = @Id AND DeletedDate IS NULL";
            return await connection.QueryFirstOrDefaultAsync<SubscriptionDetails>(query, new { Id = id });
        }

        // Create a new subscription detail
        public async Task<SubscriptionDetails> CreateSubscriptionDetail(SubscriptionDetails subscriptionDetail)
        {
            if (string.IsNullOrEmpty(subscriptionDetail.FeatureDescription))
            {
                throw new ArgumentException("FeatureDescription cannot be empty.");
            }

            if (string.IsNullOrEmpty(subscriptionDetail.DetailType))
            {
                throw new ArgumentException("DetailType cannot be empty.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                INSERT INTO SubscriptionDetails (SubscriptionID, FeatureDescription, DetailType, CreatedDate)
                VALUES (@SubscriptionID, @FeatureDescription, @DetailType, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)"; // Get the last inserted ID
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                subscriptionDetail.SubscriptionID,
                subscriptionDetail.FeatureDescription,
                subscriptionDetail.DetailType,
                subscriptionDetail.CreatedDate
            });

            subscriptionDetail.DetailID = id;  // Assign the ID to the model
            return subscriptionDetail;
        }

        // Update an existing subscription detail
        public async Task<SubscriptionDetails> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetail)
        {
            if (string.IsNullOrEmpty(subscriptionDetail.FeatureDescription))
            {
                throw new ArgumentException("FeatureDescription cannot be empty.");
            }

            if (string.IsNullOrEmpty(subscriptionDetail.DetailType))
            {
                throw new ArgumentException("DetailType cannot be empty.");
            }

            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE SubscriptionDetails
                SET SubscriptionID = @SubscriptionID,
                    FeatureDescription = @FeatureDescription,
                    DetailType = @DetailType,
                    ModifiedDate = @ModifiedDate
                WHERE DetailID = @Id AND DeletedDate IS NULL";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                subscriptionDetail.SubscriptionID,
                subscriptionDetail.FeatureDescription,
                subscriptionDetail.DetailType,
                ModifiedDate = DateTime.UtcNow,  // Set modification date
                Id = id
            });

            if (rowsAffected == 0)
            {
                return null; // No record was updated
            }

            return subscriptionDetail; // Return the updated subscription detail
        }

        // Mark a subscription detail as deleted (soft delete)
        public async Task<bool> DeleteSubscriptionDetail(int id, string deletedBy)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                UPDATE SubscriptionDetails
                SET DeletedBy = @DeletedBy,
                    DeletedDate = @DeletedDate
                WHERE DetailID = @Id AND DeletedDate IS NULL";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                DeletedBy = deletedBy,
                DeletedDate = DateTime.UtcNow, // Set deletion date
                Id = id
            });

            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"SubscriptionDetail with ID {id} not found or already deleted.");
            }

            return true;
        }
    }
}
