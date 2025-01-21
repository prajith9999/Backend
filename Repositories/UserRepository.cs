using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all users using stored procedure
        public async Task<List<User>> GetUsers()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.GetUsers";
            var users = await connection.QueryAsync<User>(query);
            return users.AsList();
        }

        // Get a user by ID using stored procedure
        public async Task<User> GetUserById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.GetUserById @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        // Create a new user using stored procedure
        public async Task<User> CreateUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.CreateUser @FirstName, @LastName, @Email";
            var id = await connection.QuerySingleAsync<int>(query, new
            {
                user.FirstName,
                user.LastName,
                user.Email
            });

            user.ID = id;
            return user;
        }

        // Update an existing user using stored procedure
        public async Task<User> UpdateUser(int id, User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.UpdateUser @Id, @FirstName, @LastName, @Email";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                Id = id,
                user.FirstName,
                user.LastName,
                user.Email
            });

            if (rowsAffected == 0)
            {
                return null; // No record was updated
            }

            user.ID = id;
            return user;
        }

        // Delete a user using stored procedure
        public async Task<bool> DeleteUser(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "EXEC dbo.DeleteUser @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
