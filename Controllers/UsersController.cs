using Microsoft.AspNetCore.Mvc;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.Filters;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ViewAdAPI.Controllers
{
    /// <summary>
    /// CRUD operations for Users table
    /// </summary>
    /// <response code="400">Returns error messages if request fails</response>
    [Authorize]
    [Route("api/users/")]
    [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBAL _usersBAL;

        public UsersController(IUsersBAL usersBAL)
        {
            _usersBAL = usersBAL;
        }

        // GET: api/users
        /// <summary>
        /// To get all users with paginated
        /// </summary>
        /// <response code="200">It will return list of users with pagination</response>
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResponse<IEnumerable<User>>>> GetUsers(PaginationRequest request)
        {
            try
            {
                return await _usersBAL.GetAllAsync(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // GET api/users/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To get a user by id
        /// </summary>
        /// <response code="200">It will return single user object</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            try
            {
                return await _usersBAL.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // POST api/users
        /// <summary>
        /// To create a user
        /// </summary>
        /// <response code="201">It will return user id(Guid) which is created</response>
        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUser request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var userId = await _usersBAL.InsertAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Coins = request.Coins,
                    DeviceToken = request.DeviceToken,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber
                });
                if (userId is null)
                    return BadRequest(new { error = "Unable to create user" });
                return CreatedAtAction(nameof(GetUser), new {id = userId}, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // PUT api/users/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To update a user
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody]CreateUser request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _usersBAL.UpdateAsync(new User
                {
                    Id = id,
                    Coins = request.Coins,
                    DeviceToken = request.DeviceToken,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber
                });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // DELETE api/users/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To delete a user
        /// </summary>
        /// <response code="200">It will return true if it is deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteUser(Guid id)
        {
            try
            {
                return await _usersBAL.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }
    }
}

