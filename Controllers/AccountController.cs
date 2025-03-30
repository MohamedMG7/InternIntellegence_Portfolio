using Microsoft.AspNetCore.Mvc;
using InternIntellegence_Portfolio.Services;
using InternIntellegence_Portfolio.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InternIntellegence_Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountManagementService _accountService;

        public AccountController(AccountManagementService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto 
                { 
                    Success = false, 
                    Message = "Invalid input", 
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList() 
                });
            }

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto 
                { 
                    Success = false, 
                    Message = "Passwords do not match" 
                });
            }

            var result = await _accountService.RegisterAsync(registerDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto 
                { 
                    Success = false, 
                    Message = "Invalid input", 
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList() 
                });
            }

            var result = await _accountService.LoginAsync(loginDto);
            if (result.Result.Succeeded)
            {
                // Return successful login response with userId
                return Ok(result);
            }

            // Return error response for failed login
            return BadRequest(result);
        }
    }
}