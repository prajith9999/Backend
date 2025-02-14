using LandWind.Models;
using LandWind.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LandWind.Handlers
{
    public interface IBodyHandler
    {
        Task<List<Body>> GetAllBodies();
        Task<Body> GetBodyById(int id);
        Task<Body> CreateBody(Body body);
        Task<Body> UpdateBody(int id, Body body);
        Task<bool> DeleteBody(int id);
        Task<object?> GetBodies();
        Task<Body> InsertBody(Body body);
    }

    public class BodyHandler : IBodyHandler
    {
        private readonly IBodyRepository _repository;
        private readonly ILogger<BodyHandler> _logger;

        public BodyHandler(IBodyRepository repository, ILogger<BodyHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Method to get all bodies
        public async Task<List<Body>> GetAllBodies()
        {
            try
            {
                return await _repository.GetBodies();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all bodies.");
                throw new InvalidOperationException("An error occurred while fetching the bodies.", ex);
            }
        }

        // Method to get body by ID
        public async Task<Body> GetBodyById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.", nameof(id));

            try
            {
                var result = await _repository.GetBodyById(id);
                if (result == null)
                {
                    throw new KeyNotFoundException($"Body with ID {id} not found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the body with ID {id}.");
                throw new InvalidOperationException($"An error occurred while fetching the body with ID {id}.", ex);
            }
        }

        // Method to create a new body
        public async Task<Body> CreateBody(Body body)
        {
            if (body == null) throw new ArgumentNullException(nameof(body), "Body cannot be null.");

            try
            {
                return await _repository.InsertBody(body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the body.");
                throw new InvalidOperationException("An error occurred while creating the body.", ex);
            }
        }

        // Method to update an existing body
        public async Task<Body> UpdateBody(int id, Body body)
        {
            if (id <= 0 || body == null) throw new ArgumentException("Invalid input for updating body.", nameof(id));

            try
            {
                return await _repository.UpdateBody(id, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the body with ID {id}.");
                throw new InvalidOperationException($"An error occurred while updating the body with ID {id}.", ex);
            }
        }

        // Method to delete a body
        public async Task<bool> DeleteBody(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.", nameof(id));

            try
            {
                return await _repository.DeleteBody(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the body with ID {id}.");
                throw new InvalidOperationException($"An error occurred while deleting the body with ID {id}.", ex);
            }
        }

        // Implementation for GetBodies method (if needed for other scenarios)
        public async Task<object?> GetBodies()
        {
            try
            { 
                return await _repository.GetAllBodies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //_logger.LogError(ex, "An error occurred while fetching the bodies.");
                //throw new InvalidOperationException("An error occurred while fetching the bodies.", ex);
            }
        }

        // InsertBody method implementation
        public async Task<Body> InsertBody(Body body)
        {
            if (body == null) throw new ArgumentNullException(nameof(body), "Body cannot be null.");

            try
            {
                return await _repository.InsertBody(body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting the body.");
                throw new InvalidOperationException("An error occurred while inserting the body.", ex);
            }
        }
    }
}
