using System;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;

namespace LandWind.Handlers
{
    public class FaqHandler : IFaqHandler
    {
        private readonly IFaqRepository _repository;

        public FaqHandler(IFaqRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Faq>> GetFaqs()
        {
            try
            {
                return await _repository.GetFaqs();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the FAQs.", ex);
            }
        }

        public async Task<Faq> GetFaqById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                var result = await _repository.GetFaqById(id);
                if (result == null)
                    throw new Exception("FAQ not found.");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the FAQ.", ex);
            }
        }

        public async Task<Faq> CreateFaq(Faq faq)
        {
            if (faq == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.CreateFaq(faq);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the FAQ.", ex);
            }
        }

        public async Task<Faq> UpdateFaq(int id, Faq faq)
        {
            if (id <= 0 || faq == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.UpdateFaq(id, faq);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the FAQ.", ex);
            }
        }

        public async Task<bool> DeleteFaq(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                return await _repository.DeleteFaq(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the FAQ.", ex);
            }
        }
    }
}
