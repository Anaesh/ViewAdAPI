using Microsoft.AspNetCore.Mvc;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.Filters;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ViewAdAPI.Controllers
{
    /// <summary>
    /// CRUD operations for Withdrawaldata table
    /// </summary>
    /// <response code="400">Returns error messages if request fails</response>
    [Authorize]
    [Route("api/withdrawaldata")]
    public class WithdrawaldataController : ControllerBase
    {
        private readonly IWithdrawaldataBAL _withdrawaldataBAL;

        public WithdrawaldataController(IWithdrawaldataBAL withdrawaldataBAL)
        {
            _withdrawaldataBAL = withdrawaldataBAL;
        }
        // GET: api/withdrawaldata
        /// <summary>
        /// To get all withdrawaldata with paginated
        /// </summary>
        /// <response code="200">It will return list of withdrawaldata with pagination</response>
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResponse<IEnumerable<WithdrawaldataResponse>>>> GetWithdrawaldata(PaginationRequest request, Guid? userId = null, Guid? tokenId = null)
        {
            try
            {
                return await _withdrawaldataBAL.GetAllAsync(request, userId, tokenId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // GET api/withdrawaldata/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To get a withdrawaldata by id
        /// </summary>
        /// <response code="200">It will return single withdrawaldata object</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<WithdrawaldataResponse>> GetWithdrawaldata(Guid id)
        {
            try
            {
                return await _withdrawaldataBAL.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // POST api/withdrawaldata
        /// <summary>
        /// To create a withdrawaldata
        /// </summary>
        /// <response code="201">It will return withdrawaldata id(Guid) which is created</response>
        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateWithdrawaldata([FromBody] CreateWithdrawaldata request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var withdrawaldataId = await _withdrawaldataBAL.InsertAsync(new Withdrawaldata
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Amount = request.Amount,
                    Link = request.Link,
                    RequestStatus = request.RequestStatus,
                    TokenId = request.TokenId,
                    WithdrawDateTime = request.WithdrawDateTime
                });
                if (withdrawaldataId is null)
                    return BadRequest(new { error = "Unable to create Withdrawaldata" });
                return CreatedAtAction(nameof(GetWithdrawaldata), new { id = withdrawaldataId }, withdrawaldataId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // PUT api/withdrawaldata/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To update a withdrawaldata
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateWithdrawaldata(Guid id, [FromBody] CreateWithdrawaldata request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _withdrawaldataBAL.UpdateAsync(new Withdrawaldata
                {
                    Id = id,
                    UserId = request.UserId,
                    Amount = request.Amount,
                    Link = request.Link,
                    RequestStatus = request.RequestStatus,
                    TokenId = request.TokenId,
                    WithdrawDateTime = request.WithdrawDateTime
                });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // DELETE api/withdrawaldata/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To delete a withdrawaldata
        /// </summary>
        /// <response code="200">It will return true if it is deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteWithdrawaldata(Guid id)
        {
            try
            {
                return await _withdrawaldataBAL.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }
    }
}

