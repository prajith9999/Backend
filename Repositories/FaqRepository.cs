using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class FaqRepository
    {
        private readonly AppHandler _appHandler;

        public FaqRepository(AppHandler appHandler)
        {
            _appHandler = appHandler ?? throw new ArgumentNullException(nameof(appHandler));
        }

        public async Task<List<Faq>> GetFaqs()
        {
            try
            {
                var query = "SELECT * FROM dbo.faq";
                return await _appHandler.ExecuteQueryAsync<Faq>(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching FAQs", ex);
            }
        }

        public async Task<Faq> GetFaqById(int id)
        {
            try
            {
                var query = "SELECT * FROM dbo.faq WHERE ID = @Id";
                return await _appHandler.ExecuteQueryFirstOrDefaultAsync<Faq>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching FAQ with ID {id}", ex);
            }
        }

        public AppHandler Get_appHandler()
        {
            return _appHandler;
        }

        public async Task<Faq> CreateFaq(Faq faq, AppHandler _appHandler)
        {
            try
            {
                var query = @"
                    INSERT INTO dbo.faq (Title, Question, Answer, CreatedDate) 
                    VALUES (@Title, @Question, @Answer, @CreatedDate);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
                faq.ID = await _appHandler.ExecuteScalarAsync<int>(query, new
                {
                    faq.Title,
                    faq.Question,
                    faq.Answer,
                    CreatedDate = DateTime.UtcNow
                });
                return faq;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating FAQ", ex);
            }
        }

        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            try
            {
                var query = @"
                    UPDATE dbo.faq
                    SET Title = @Title, Question = @Question, Answer = @Answer, ModifiedDate = @ModifiedDate
                    WHERE ID = @Id";
                var rowsAffected = await _appHandler.ExecuteNonQueryAsync(query, new
                {
                    faq.Title,
                    faq.Question,
                    faq.Answer,
                    ModifiedDate = DateTime.UtcNow,
                    Id = id
                });
                return rowsAffected > 0 ? faq : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating FAQ with ID {id}", ex);
            }
        }

        public async Task<bool> DeleteFaq(int id)
        {
            try
            {
                var query = "DELETE FROM dbo.faq WHERE ID = @Id";
                var rowsAffected = await _appHandler.ExecuteNonQueryAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting FAQ with ID {id}", ex);
            }
        }
    }
}
