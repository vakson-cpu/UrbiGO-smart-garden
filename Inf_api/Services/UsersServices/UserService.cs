using Inf_Data.Entities;
using Inf_Repository.Repository.User;
using Inf_Transfer.utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Inf_api.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;    
        }

        public async Task<QueryResponse<AppUser>> GetUsers(int pageNumber, int pageSize = 10)
        {
            // Ensure page number is at least 1
            if (pageNumber < 1) pageNumber = 1;

            // Calculate the number of records to skip
            int skip = (pageNumber - 1) * pageSize;

            // Retrieve users with pagination
            var userQuery = _userRepository.GetAll();
            var totalCount = userQuery.Count();
            userQuery = userQuery
                .Skip(skip)    // Skip the records of previous pages
                .Take(pageSize); // Take the records for the current page

            // Return the results as a list
            var users =  await userQuery.ToListAsync();
            var result = new QueryResponse<AppUser>()
            {
                Items = users,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
            return result;
            
        }

    }
}
