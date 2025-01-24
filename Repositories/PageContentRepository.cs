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
    public class PageContentRepository : IPageContentRepository
    {
        private readonly SqlConnection _connection;

        public PageContentRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<PageContent>> GetAll()
        {
            var query = "SELECT * FROM PageContents";
            var result = await _connection.QueryAsync<PageContent>(query);
            return (List<PageContent>)result;
        }

        public async Task<PageContent> GetById(int id)
        {
            var query = "SELECT * FROM PageContents WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<PageContent>(query, new { id });
            return result;
        }

        public async Task<PageContent> Create(PageContent item)
        {
            var query = "INSERT INTO PageContents (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<PageContent> Update(int id, PageContent item)
        {
            var query = "UPDATE PageContents SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM PageContents WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
