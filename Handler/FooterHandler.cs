using LandWind.Models;
using LandWind.Interfaces;
using LandWind.Repositories;
using LandWind.Exceptions; 

namespace LandWind.Handlers
{
    public interface IFooterHandler
    {
        Task<Footer> GetFooter();
        Task<Footer> GetFooterById(int id);
        Task<Footer> CreateFooter(Footer footer);
        Task<Footer> UpdateFooter(int id, Footer footer);
        Task<bool> DeleteFooter(int id);
    }

    public class FooterHandler : IFooterHandler
    {
        private readonly IFooterRepository _repository;

        public FooterHandler(IFooterRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Get footer data
        public async Task<Footer> GetFooter()
        {
            try
            {
                var footer = await _repository.GetFooter();
                if (footer == null)
                {
                    throw new NotFoundException("Footer not found.");
                }
                return footer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the footer.", ex);
            }
        }

        // Get footer by ID
        public async Task<Footer> GetFooterById(int id)
        {
            if (id <= 0) throw new InvalidInputException("Invalid ID provided.");

            try
            {
                var footer = await _repository.GetFooterById(id);
                if (footer == null)
                {
                    throw new NotFoundException("Footer not found.");
                }
                return footer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the footer by ID.", ex);
            }
        }

        // Create a new footer
        public async Task<Footer> CreateFooter(Footer footer)
        {
            if (footer == null) throw new InvalidInputException("Invalid input.");

            try
            {
                return await _repository.CreateFooter(footer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the footer.", ex);
            }
        }

        // Update existing footer
        public async Task<Footer> UpdateFooter(int id, Footer footer)
        {
            if (id <= 0 || footer == null) throw new InvalidInputException("Invalid input.");

            try
            {
                return await _repository.UpdateFooter(id, footer);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the footer.", ex);
            }
        }

        // Delete footer by ID
        public async Task<bool> DeleteFooter(int id)
        {
            if (id <= 0) throw new InvalidInputException("Invalid ID provided.");

            try
            {
                var isDeleted = await _repository.DeleteFooter(id);
                if (!isDeleted)
                {
                    throw new NotFoundException("Footer not found.");
                }
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the footer.", ex);
            }
        }
    }
}
