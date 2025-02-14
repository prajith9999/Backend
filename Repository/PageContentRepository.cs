using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;
using System.Data;

namespace LandWind.Repositories
{
    // Correctly named interface
    public interface IPageContentRepository
    {
        Task<List<PageContent>> GetPageContents();
        Task<PageContent> GetPageContentById(int id);
        Task<PageContent> CreatePageContent(PageContent item);
        Task<PageContent> UpdatePageContent(int id, PageContent item);
        Task<bool> DeletePageContent(int id);

    }

    // Repository implementation
    public class PageContentRepository : IPageContentRepository
    {
        private readonly IDbConnection _connection;

        public PageContentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<PageContent>> GetPageContents()
        {
            var query = "SELECT * FROM PageContent";
            var result = await _connection.QueryAsync<PageContent>(query);
            return (List<PageContent>)result;
        }

        public async Task<PageContent> GetPageContentById(int id)
        {
            var query = "SELECT * FROM PageContent WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<PageContent>(query, new { id });
            return result;
        }

        public async Task<PageContent> CreatePageContent(PageContent item)
        {
            var query = "INSERT INTO PageContent (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id;
            return item;
        }

        public async Task<PageContent> UpdatePageContent(int id, PageContent item)
        {
            var query = "UPDATE PageContent SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.Title, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        public async Task<bool> DeletePageContent(int id)
        {
            var query = "DELETE FROM PageContent WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
