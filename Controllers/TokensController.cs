using Microsoft.AspNetCore.Mvc;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.Filters;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ViewAdAPI.Controllers
{
    /// <summary>
    /// CRUD operations for Tokens table
    /// </summary>
    /// <response code="400">Returns error messages if request fails</response>
    [Authorize]
    [Route("api/tokens")]
    [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
    public class TokensController : ControllerBase
    {
        private readonly ITokensBAL _tokensBAL;

        public TokensController(ITokensBAL tokensBAL)
        {
            _tokensBAL = tokensBAL;
        }
        // GET: api/tokens
        /// <summary>
        /// To get all tokens with paginated
        /// </summary>
        /// <response code="200">It will return list of tokens with pagination</response>
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResponse<IEnumerable<Token>>>> GetTokens(PaginationRequest request)
        {
            try
            {
                return await _tokensBAL.GetAllAsync(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // GET api/tokens/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To get a token by id
        /// </summary>
        /// <response code="200">It will return single token object</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Token>> GetToken(Guid id)
        {
            try
            {
                return await _tokensBAL.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // POST api/tokens
        /// <summary>
        /// To create a token
        /// </summary>
        /// <response code="201">It will return token id(Guid) which is created</response>
        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateToken([FromBody] CreateToken request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var tokenId = await _tokensBAL.InsertAsync(new Token
                {
                    Id = Guid.NewGuid(),
                    CurrentRewardValue = request.CurrentRewardValue,
                    Image = request.Image,
                    IsActive = request.IsActive,
                    MinimumWithdrawl = request.MinimumWithdrawl,
                    Symbol = request.Symbol,
                    TokenId = request.TokenId,
                    TokenName = request.TokenName
                });
                if (tokenId is null)
                    return BadRequest(new { error = "Unable to create Token" });
                return CreatedAtAction(nameof(GetToken), new { id = tokenId }, tokenId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // PUT api/tokens/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To update a token
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateToken(Guid id, [FromBody] CreateToken request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _tokensBAL.UpdateAsync(new Token
                {
                    Id = id,
                    CurrentRewardValue = request.CurrentRewardValue,
                    Image = request.Image,
                    IsActive = request.IsActive,
                    MinimumWithdrawl = request.MinimumWithdrawl,
                    Symbol = request.Symbol,
                    TokenId = request.TokenId,
                    TokenName = request.TokenName
                });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // DELETE api/tokens/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To delete a token
        /// </summary>
        /// <response code="200">It will return true if it is deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteToken(Guid id)
        {
            try
            {
                return await _tokensBAL.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }
    }
}

