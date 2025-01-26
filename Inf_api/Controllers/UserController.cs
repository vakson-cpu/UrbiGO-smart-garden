using Inf_api.Services.Auth;
using Inf_Repository.Repository.UnitOfWork;
using Inf_Transfer.DTOS.UserDTOs;
using Inf_Transfer.utils;
using Microsoft.AspNetCore.Mvc;

namespace Inf_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IAuthService _authService { get; set; }

        public IUnitOfWork _unitOfWork { get; set; }

        public UserController(IAuthService authService,IUnitOfWork unitOfWork)
        {
            this._authService = authService;
            this._unitOfWork = unitOfWork;  
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                string message = Extensions.EnumExtension.GetEnumDescription(
                    ResponseMessages.VALIDATION_ERROR
                );
                return BadRequest(new CustomResponse<string>(message, false, message));
            }
            return Ok(await this._authService.Register(newUser));
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginCredentials user)
        {
            var result = await this._authService.LogIn(user);
            if (result.succeeded)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _unitOfWork.UserRepository.GetUsers();
            return Ok(result);
        }

    }
}
