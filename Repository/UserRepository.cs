//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LandWind.Interfaces;
//using LandWind.Models;
//using LandWind.Repositories; // Correct place for this using directive

//namespace LandWind.Handlers
//{
//    public class UserHandler
//    {
//        private readonly IUserRepository _userRepository;

//        // Inject the repository into the handler
//        public UserHandler(IUserRepository userRepository)
//        {
//            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
//        }

//        // Get a user by ID
//        public async Task<User> GetUserByIdAsync(int id)
//        {
//            if (id <= 0) throw new ArgumentException("Invalid user ID.", nameof(id));

//            try
//            {
//                var user = await _userRepository.GetUserByIdAsync(id);
//                if (user == null)
//                {
//                    throw new Exception("User not found.");
//                }

//                return user;
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or rethrow based on your needs
//                throw new Exception($"An error occurred while retrieving the user: {ex.Message}", ex);
//            }
//        }

//        // Get all users
//        public async Task<IEnumerable<User>> GetAllUsersAsync()
//        {
//            try
//            {
//                return await _userRepository.GetAllUsersAsync();
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or rethrow based on your needs
//                throw new Exception($"An error occurred while retrieving users: {ex.Message}", ex);
//            }
//        }

//        // Create a new user
//        public async Task<User> CreateUserAsync(User user)
//        {
//            if (user == null) throw new ArgumentNullException(nameof(user));

//            try
//            {
//                return await _userRepository.CreateUserAsync(user);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or rethrow based on your needs
//                throw new Exception($"An error occurred while creating the user: {ex.Message}", ex);
//            }
//        }

//        // Update an existing user
//        public async Task<User> UpdateUserAsync(User user)
//        {
//            if (user == null || user.ID <= 0) throw new ArgumentException("Invalid user details.");

//            try
//            {
//                return await _userRepository.UpdateUserAsync(user);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or rethrow based on your needs
//                throw new Exception($"An error occurred while updating the user: {ex.Message}", ex);
//            }
//        }

//        // Delete a user by ID
//        public async Task<bool> DeleteUserAsync(int id)
//        {
//            if (id <= 0) throw new ArgumentException("Invalid user ID.", nameof(id));

//            try
//            {
//                return await _userRepository.DeleteUserAsync(id);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or rethrow based on your needs
//                throw new Exception($"An error occurred while deleting the user: {ex.Message}", ex);
//            }
//        }
//    }
//}
