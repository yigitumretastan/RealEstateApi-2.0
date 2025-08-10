using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiServices.Contacts;
using Microsoft.AspNetCore.Authorization;
using RealEstateApiCore.DTOs;

namespace RealEstateApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                var usersdto = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }).ToList();
                return Ok(usersdto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                var userdto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
                return Ok(userdto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                    return BadRequest("Registration data is required");

                var user = new User
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    Password = registerDto.Password
                };

                var createdUser = await userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                    return BadRequest("Login data is required");

                var user = await userService.LoginAsync(loginDto.Email, loginDto.Password);
                if (user == null)
                    return Unauthorized("Invalid email or password");

                var token = user.Token;
                return Ok(new { token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                if (updateUserDto == null)
                    return BadRequest("Update data is required");

                var user = new User
                {
                    Id = id,
                    Name = updateUserDto.Name,
                    Email = updateUserDto.Email,
                    Password = updateUserDto.Password
                };

                var updatedUser = await userService.UpdateUserAsync(id, user);
                if (updatedUser == null)
                    return NotFound();

                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await userService.DeleteUserAsync(id);
                if (deletedUser == null)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}