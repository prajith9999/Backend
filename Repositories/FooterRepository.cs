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
    public class FooterRepository : IFooterRepository
    {
        private readonly SqlConnection _connection;

        public FooterRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Footer>> GetAll()
        {
            var query = "SELECT * FROM Footers";
            var result = await _connection.QueryAsync<Footer>(query);
            return (List<Footer>)result;
        }

        public async Task<Footer> GetById(int id)
        {
            var query = "SELECT * FROM Footers WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Footer>(query, new { id });
            return result;
        }

        public async Task<Footer> Create(Footer item)
        {
            var query = "INSERT INTO Footers (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<Footer> Update(int id, Footer item)
        {
            var query = "UPDATE Footers SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Footers WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
