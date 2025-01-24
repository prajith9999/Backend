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
    public class BodyRepository : IBodyRepository
    {
        private readonly SqlConnection _connection;

        public BodyRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Body>> GetAll()
        {
            var query = "SELECT * FROM Bodys";
            var result = await _connection.QueryAsync<Body>(query);
            return (List<Body>)result;
        }

        public async Task<Body> GetById(int id)
        {
            var query = "SELECT * FROM Bodys WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Body>(query, new { id });
            return result;
        }

        public async Task<Body> Create(Body item)
        {
            var query = "INSERT INTO Bodys (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<Body> Update(int id, Body item)
        {
            var query = "UPDATE Bodys SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Bodys WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
