using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;
// Add this at the top of your FooterRepository file


namespace LandWind.Repositories
{
    // Interface definition for Footer repository
    public interface IFooterRepository
    {
        Task<List<Footer>> GetAll();                         // Get all footers
        Task<Footer> GetById(int id);                        // Get footer by ID
        Task<Footer> Create(Footer item);                    // Create a new footer
        Task<Footer> Update(int id, Footer item);            // Update an existing footer
        Task<bool> Delete(int id);                           // Delete a footer by ID

        // Additional methods for Footer
        Task<Footer> GetFooter();                            // Fetch a single footer
        Task<Footer> GetFooterById(int id);                  // Fetch footer by ID
        Task<Footer> CreateFooter(Footer footer);            // Create a footer
        Task<Footer> UpdateFooter(int id, Footer footer);    // Update footer by ID
        Task<bool> DeleteFooter(int id);                     // Delete footer by ID
        Task<object?> GetAllFooter();                        // Fetch all footers as an object
    }

    // Implementation of FooterRepository
    public class FooterRepository : IFooterRepository
    {
        private readonly IDbConnection _connection;

        // Constructor to inject the database connection
        public FooterRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Get all footers
        public async Task<List<Footer>> GetAll()
        {
            var query = "SELECT * FROM Footer";
            var result = await _connection.QueryAsync<Footer>(query);
            return result.AsList();  // Ensuring it returns List<Footer>
        }

        // Get footer by ID
        public async Task<Footer> GetById(int id)
        {
            var query = "SELECT * FROM Footer WHERE ID = @id";
            var result = await _connection.QueryFirstOrDefaultAsync<Footer>(query, new { id });
            return result;
        }

        // Create a new footer
        public async Task<Footer> Create(Footer item)
        {
            var query = "INSERT INTO Footer (Title, CreatedBy, CreatedDate) VALUES (@Title, @CreatedBy, @CreatedDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = await _connection.ExecuteScalarAsync<int>(query, item);
            item.ID = id; // Assign the generated ID to the item
            return item;
        }

        // Update an existing footer
        public async Task<Footer> Update(int id, Footer item)
        {
            var query = "UPDATE Footer SET Title = @Title, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE ID = @id";
            await _connection.ExecuteAsync(query, new { item.FooterTitle, item.ModifiedBy, item.ModifiedDate, id });
            return item;
        }

        // Delete a footer by ID
        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Footer WHERE ID = @id";
            var rowsAffected = await _connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }

        // Additional method to fetch footer by ID
        public async Task<Footer> GetFooterById(int id)
        {
            return await GetById(id); // Use the GetById method to fetch the footer
        }

        // Create a new footer
        public async Task<Footer> CreateFooter(Footer footer)
        {
            return await Create(footer); // Use Create method for inserting a new footer
        }

        // Update an existing footer by ID
        public async Task<Footer> UpdateFooter(int id, Footer footer)
        {
            return await Update(id, footer); // Use Update method for updating footer
        }

        // Delete a footer by ID
        public async Task<bool> DeleteFooter(int id)
        {
            return await Delete(id); // Use Delete method for deleting a footer by ID
        }

        // Fetch all footers as an object
        public async Task<object?> GetAllFooter()
        {
            return await GetAll(); // Use GetAll method to return footers as an object
       }

        public async Task<Footer> GetFooter()//change the ode here 
        {
            var query = "SELECT TOP 1 * FROM Footer";  // Adjust the query as needed
            var result = await _connection.QueryFirstOrDefaultAsync<Footer>(query);
            return result;
        }

    }
}
