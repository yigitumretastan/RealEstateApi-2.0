using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await userRepository.GetUserById(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                var createdUser = await userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest();

                var updatedUser = await userRepository.UpdateUser(id, user);
                if (updatedUser == null)
                    return NotFound();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await userRepository.DeleteUser(id);
                if (deletedUser == null)
                    return NotFound();

                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
