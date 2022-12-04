using ClinicAPI.Data.Dtos;
using ClinicAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicAPI.Data.Repositories;
using ClinicAPI.Data;
using System.Text.Json;
using ClinicAPI.Auth.Models;
using Microsoft.AspNetCore.Identity;
using ClinicAPI.Auth;
using Microsoft.AspNetCore.Authorization;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ClinicUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAuthorizationService _authorizationService;

        public AuthController(UserManager<ClinicUser> userManager, IJwtTokenService jwtTokenService, IAuthorizationService authService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _authorizationService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerDto)
        {
            var user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user != null)
                return BadRequest("User already exists!");

            var newUser = new ClinicUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                return BadRequest(createUserResult.Errors);
            }
            await _userManager.AddToRoleAsync(newUser, ClinicRoles.User);

            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));


        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("Wrong password or username !");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest("Wrong password or username!");

            // the user is valid at this point

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new SuccessfulLoginDto(accessToken));

        }
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = ClinicRoles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            var admin = await _userManager.FindByNameAsync("admin");
            var users = await _userManager.GetUsersInRoleAsync("User");
            users.Remove(admin);
            return Ok(users.Select(user => new UserDto(user.Id, user.UserName, user.Email)));
        }
        [HttpGet]
        [Route("employees")]
        [Authorize(Roles = ClinicRoles.Admin)]
        public async Task<IActionResult> GetEmployees()
        {
            var admin = await _userManager.FindByNameAsync("admin");
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            users.Remove(admin);
            return Ok(users.Select(user => new UserDto(user.Id, user.UserName, user.Email)));
        }
        [HttpDelete]
        [Route("remove")]
        [Authorize(Roles = ClinicRoles.Admin)]
        public async Task<IActionResult> Remove(SearchDto searchDto)
        {
            var found = await _userManager.FindByIdAsync(searchDto.Id);
            if(found == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(found);
            return Ok();
        }
        [HttpPut]
        [Route("update")]
        [Authorize(Roles = ClinicRoles.Admin)]
        public async Task<IActionResult> ChangeRole(SearchDto searchDto)
        {
            var found = await _userManager.FindByIdAsync(searchDto.Id);
            if (found == null)
            {
                return NotFound();
            }

            /// user is found
            bool flag = await _userManager.IsInRoleAsync(found, "User");
            if (flag)
            {
                await _userManager.AddToRoleAsync(found, "Employee");
                await _userManager.RemoveFromRoleAsync(found, "User");
            }
            else
            {
                await _userManager.AddToRoleAsync(found, "User");
                await _userManager.RemoveFromRoleAsync(found, "Employee");
            }
            return Ok();
        }

    }
}
