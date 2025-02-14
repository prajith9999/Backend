using LandWind.Models;
using LandWind.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LandWind.Handlers
{
    public interface IFaqHandler
    {
        Task<List<Faq>> GetAllFaqs(); 
        Task<Faq> GetFaqById(int id);
        Task<Faq> CreateFaq(Faq faq);
        Task<Faq> UpdateFaq(int id, Faq faq);
        Task<bool> DeleteFaq(int id);
    }

    public class FaqHandler : IFaqHandler
    {
        private readonly IFaqRepository _repository;
        private readonly ILogger<FaqHandler> _logger;

        public FaqHandler(IFaqRepository repository, ILogger<FaqHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Faq>> GetAllFaqs()
        {
            try
            {
                return await _repository.GetAll();  // Use GetAll instead of GetAllFaqs
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all FAQs.");
                throw new InvalidOperationException("An error occurred while fetching the FAQs.", ex);
            }
        }

        public async Task<Faq> GetFaqById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.", nameof(id));

            try
            {
                var result = await _repository.GetFaqById(id);
                if (result == null)
                {
                    throw new KeyNotFoundException($"FAQ with ID {id} not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the FAQ with ID {id}.");
                throw new InvalidOperationException($"An error occurred while fetching the FAQ with ID {id}.", ex);
            }
        }

        public async Task<Faq> CreateFaq(Faq faq)
        {
            if (faq == null) throw new ArgumentNullException(nameof(faq), "FAQ cannot be null.");

            try
            {
                return await _repository.CreateFaq(faq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the FAQ.");
                throw new InvalidOperationException("An error occurred while creating the FAQ.", ex);
            }
        }

        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            if (id <= 0 || faq == null) throw new ArgumentException("Invalid input for updating FAQ.", nameof(id));

            try
            {
                return await _repository.UpdateFaq(id, faq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the FAQ with ID {id}.");
                throw new InvalidOperationException($"An error occurred while updating the FAQ with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteFaq(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.", nameof(id));

            try
            {
                return await _repository.DeleteFaq(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the FAQ with ID {id}.");
                throw new InvalidOperationException($"An error occurred while deleting the FAQ with ID {id}.", ex);
            }
        }
    }
}
