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

        // Get all users
        public async Task<List<User>> GetUsers()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.[User]";
            var users = await connection.QueryAsync<User>(query);
            return users.AsList();
        }

        // Get a user by ID
        public async Task<User> GetUserById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.[User] WHERE ID = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        // Create a new user
        public async Task<User> CreateUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO dbo.[User] (Username, Email, Password, FullName, PhoneNumber, CreatedBy, CreatedDate) 
                          OUTPUT INSERTED.ID 
                          VALUES (@Username, @Email, @Password, @FullName, @PhoneNumber, @CreatedBy, @CreatedDate)";

            var id = await connection.QuerySingleAsync<int>(query, new
            {
                user.Username,
                user.Email,
                user.Password,
                user.FullName,
                user.PhoneNumber,
                user.CreatedBy,
                user.CreatedDate
            });

            user.ID = id;
            return user;
        }

        // Update an existing user
        public async Task<User> UpdateUser(int id, User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE dbo.[User] 
                          SET Username = @Username, Email = @Email, Password = @Password, FullName = @FullName, 
                              PhoneNumber = @PhoneNumber, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate 
                          WHERE ID = @Id";

            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                Id = id,
                user.Username,
                user.Email,
                user.Password,
                user.FullName,
                user.PhoneNumber,
                user.ModifiedBy,
                user.ModifiedDate
            });

            if (rowsAffected == 0)
            {
                return null; // No record was updated
            }

            user.ID = id;
            return user;
        }

        // Delete a user
        public async Task<bool> DeleteUser(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM dbo.[User] WHERE ID = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
