using Inf_Repository.Repository.User;

namespace Inf_api.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepostiroy;

        public UserService(IUserRepository userRepository)
        {
            _userRepostiroy= userRepository;    
        }
    }
}
