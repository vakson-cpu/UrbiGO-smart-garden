using Inf_api.Extensions;
using Inf_Data.Entities;
using Inf_Repository.Repository.UnitOfWork;
using Inf_Transfer.DTOS.UserDTOs;
using Inf_Transfer.utils;
using Microsoft.AspNetCore.Identity;

namespace Inf_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        public UserManager<AppUser> _userManager { get; set; }
        
        
        public IUnitOfWork _unitOfWork { get; set; }
        public ITokenService _tokenService { get; set; }

        public AuthService(
            UserManager<AppUser> userManager,
            IUnitOfWork unitOfwork,
            ITokenService tokenService
        )
        {
            this._userManager = userManager;
            this._unitOfWork = unitOfwork;
            this._tokenService = tokenService;
        }

        public async Task<CustomResponse<AuthResponseDTO>> LogIn(LoginCredentials creds)
        {
            var user = await this._userManager.FindByEmailAsync(creds.Email);
            if (user == null)
                throw new BadHttpRequestException(
                    Extensions.EnumExtension.GetEnumDescription(ResponseMessages.DATA_IS_NULL)
                );

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, creds.Password);
            if (!userHasValidPassword)
            {
                throw new BadHttpRequestException(
                    Extensions.EnumExtension.GetEnumDescription(ResponseMessages.VALIDATION_ERROR)
                        + "- Password is invalid"
                );
            }
            var roleList = await _userManager.GetRolesAsync(user);
            
            var role = roleList.FirstOrDefault();

            var token = await _tokenService.GenerateToken(user, role != null ? role : "User");
            if (token == null)
            {
                throw new Exception(
                    Extensions.EnumExtension.GetEnumDescription(ResponseMessages.REQUEST_FAILED)
                );
            }
            AuthResponseDTO responseData = new   AuthResponseDTO(token,user.Id,role);
            return new CustomResponse<AuthResponseDTO>(EnumExtension.GetEnumDescription(ResponseMessages.AUTH_SUCCEEDED),true,responseData);
        }

        public async Task<CustomResponse<string>> Register(CreateUserDTO newUser)
        {
            try
            {
                AppUser user = new AppUser() { Email = newUser.Email, UserName = newUser.UserName };
                var result = await this._userManager.CreateAsync(user, newUser.Password);
                await this._userManager.AddToRoleAsync(user, "User");
                await this._unitOfWork.SaveChanges();
                var message = Extensions.EnumExtension.GetEnumDescription(
                    ResponseMessages.REGISTRATION_SUCCEEDED
                );
                return new CustomResponse<string>(message, true, message);
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
