using System;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LandWind.Repositories;

namespace LandWind.Handlers
{
    public class BodyHandler : IBodyHandler
    {
        private readonly IBodyRepository _repository;

        public BodyHandler(IBodyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Body>> GetAllBodies()
        {
            try
            {
                return await _repository.GetBodies();
            }
            catch (Exception ex)
            {
                // Handle the error gracefully
                throw new Exception("An error occurred while fetching the bodies.", ex);
            }
        }

        public async Task<Body> GetBodyById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                var result = await _repository.GetBodyById(id);
                if (result == null)
                    throw new Exception("Body not found.");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the body.", ex);
            }
        }

        public async Task<Body> CreateBody(Body body)
        {
            if (body == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.InsertBody(body);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the body.", ex);
            }
        }

        public async Task<Body> UpdateBody(int id, Body body)
        {
            if (id <= 0 || body == null) throw new ArgumentException("Invalid input.");

            try
            {
                return await _repository.UpdateBody(id, body);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the body.", ex);
            }
        }

        public async Task<bool> DeleteBody(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID provided.");

            try
            {
                return await _repository.DeleteBody(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the body.", ex);
            }
        }
    }
}
