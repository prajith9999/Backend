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
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly SqlConnection _connection;

        public SocialMediaRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<SocialMedia>> GetAll()
        {
            var query = "SELECT * FROM SocialMedias";
            var result = await _connection.QueryAsync<SocialMedia>(query);
            return (List<SocialMedia>)result;
        }

        public async Task<SocialMedia> GetById(int id)
        {
            var query = "SELECT * FROM SocialMedias WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<SocialMedia>(query, new { id });
            return result;
        }

        public async Task<SocialMedia> Create(SocialMedia item)
        {
            var query = "INSERT INTO SocialMedias (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<SocialMedia> Update(int id, SocialMedia item)
        {
            var query = "UPDATE SocialMedias SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM SocialMedias WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
