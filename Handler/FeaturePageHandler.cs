using LandWind.Models;
//using LandWind.Interfaces;
using LandWind.Repositories;

namespace LandWind.Handler
{
    public interface IFeaturePageHandler
    {
        Task<List<FeaturePage>> GetFeaturePages();
        Task<FeaturePage> GetFeaturePageById(int id);
        Task<FeaturePage> CreateFeaturePage(FeaturePage featurePage);
        Task<FeaturePage> UpdateFeaturePage(int id, FeaturePage featurePage);
        Task<bool> DeleteFeaturePage(int id);
        Task GetFeatureById(int id);
    }
    public class FeaturePageHandler : IFeaturePageHandler
    {
        private readonly IFeaturePageRepository _repository;

        // Constructor for dependency injection
        public FeaturePageHandler(IFeaturePageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Get all feature pages
        public async Task<List<FeaturePage>> GetFeaturePages()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the feature pages.", ex);
            }
        }

        // Get feature page by ID
        public async Task<FeaturePage> GetFeaturePageById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                var result = await _repository.GetById(id);
                if (result == null)
                    throw new Exception("FeaturePage not found.");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the feature page.", ex);
            }
        }

        // Create a new feature page
        public async Task<FeaturePage> CreateFeaturePage(FeaturePage featurePage)
        {
            if (featurePage == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.Create(featurePage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the feature page.", ex);
            }
        }

        // Update an existing feature page
        public async Task<FeaturePage> UpdateFeaturePage(int id, FeaturePage featurePage)
        {
            if (id <= 0 || featurePage == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.Update(id, featurePage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the feature page.", ex);
            }
        }

        // Delete feature page by ID
        public async Task<bool> DeleteFeaturePage(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                return await _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the feature page.", ex);
            }
        }

        public Task GetFeatureById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
