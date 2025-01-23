using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class BodyRepository
    {
        private readonly string _connectionString;

        public BodyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Body>> GetBodies()
        {
            var handler = new DbHandler(_connectionString);
            var query = "SELECT * FROM Bodies";
            return await handler.ExecuteQueryAsync<Body>(query);
        }

        public async Task<Body> GetBodyById(int id)
        {
            var handler = new DbHandler(_connectionString);
            var query = "SELECT * FROM Bodies WHERE Id = @Id";
            var parameters = new { Id = id };
            var body = await handler.ExecuteQueryFirstOrDefaultAsync<Body>(query, parameters);

            if (body == null)
            {
                throw new KeyNotFoundException($"Body with ID {id} not found.");
            }

            return body;
        }

        public async Task<Body> InsertBody(Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            body.CreatedDate ??= DateTime.UtcNow;

            var handler = new DbHandler(_connectionString);
            var query = @"
                INSERT INTO Bodies (Title, TitleDescription, OrderNumber, CreatedBy, CreatedDate)
                VALUES (@Title, @TitleDescription, @OrderNumber, @CreatedBy, @CreatedDate);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new { body.Title, body.TitleDescription, body.OrderNumber, body.CreatedBy, body.CreatedDate };
            var id = await handler.ExecuteScalarAsync<int>(query, parameters);

            body.ID = id;
            return body;
        }

        public async Task<Body> UpdateBody(int id, Body body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body cannot be null.");
            }

            var handler = new DbHandler(_connectionString);
            var query = "SELECT * FROM Bodies WHERE Id = @Id";
            var parameters = new { Id = id };
            var existingBody = await handler.ExecuteQueryFirstOrDefaultAsync<Body>(query, parameters);

            if (existingBody == null)
            {
                throw new KeyNotFoundException($"Body with ID {id} not found.");
            }

            var updateQuery = @"
                UPDATE Bodies
                SET Title = @Title,
                    TitleDescription = @TitleDescription,
                    OrderNumber = @OrderNumber,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE Id = @Id";

            var updateParameters = new
            {
                body.Title,
                body.TitleDescription,
                body.OrderNumber,
                body.ModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                Id = id
            };

            await handler.ExecuteAsync(updateQuery, updateParameters);

            return body;
        }

        public async Task<bool> DeleteBody(int id)
        {
            var handler = new DbHandler(_connectionString);
            var query = "DELETE FROM Bodies WHERE Id = @Id";
            var parameters = new { Id = id };
            var rowsAffected = await handler.ExecuteAsync(query, parameters);

            return rowsAffected > 0;
        }
    }

    public class DbHandler
    {
        private readonly string _connectionString;

        public DbHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> ExecuteQueryAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return (await connection.QueryAsync<T>(query, parameters)).AsList();
        }

        public async Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.ExecuteScalarAsync<T>(query, parameters);
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.ExecuteAsync(query, parameters);
        }
    }
}