using System;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LandWind.Handlers
{
    // Corrected the inheritance issue by removing : FooterHandler
    public class FooterHandler
    {
        private readonly IFooterRepository _repository;

        // Constructor to inject the IFooterRepository
        public FooterHandler(IFooterRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Get footer data
        public async Task<Footer> GetFooter()
        {
            var footer = await _repository.GetFooter();
            if (footer == null) throw new NotFoundException("Footer not found.");
            return footer;
        }

        // Get footer by ID
        public async Task<Footer> GetFooterById(int id)
        {
            if (id <= 0) throw new InvalidInputException("Invalid ID provided.");

            var footer = await _repository.GetFooterById(id);
            if (footer == null) throw new NotFoundException("Footer not found.");

            return footer;
        }

        // Create a new footer
        public async Task<Footer> CreateFooter(Footer footer)
        {
            if (footer == null) throw new InvalidInputException("Invalid input.");

            return await _repository.CreateFooter(footer);
        }

        // Update footer
        public async Task<Footer> UpdateFooter(int id, Footer footer)
        {
            if (id <= 0 || footer == null) throw new InvalidInputException("Invalid input.");

            return await _repository.UpdateFooter(id, footer);
        }

        // Delete footer by ID
        public async Task<bool> DeleteFooter(int id)
        {
            if (id <= 0) throw new InvalidInputException("Invalid ID provided.");

            var isDeleted = await _repository.DeleteFooter(id);
            if (!isDeleted) throw new NotFoundException("Footer not found.");

            return isDeleted;
        }
    }

    // Custom exception for NotFound errors
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    // Custom exception for invalid input errors
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
}
