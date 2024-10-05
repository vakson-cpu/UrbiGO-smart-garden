using Inf_Transfer.DTOS.UserDTOs;
using Inf_Transfer.utils;

namespace Inf_api.Services.Auth;

public interface IAuthService
{
    Task<CustomResponse<AuthResponseDTO>> LogIn(LoginCredentials creds);

    Task<CustomResponse<string>> Register(CreateUserDTO newUser);    


}