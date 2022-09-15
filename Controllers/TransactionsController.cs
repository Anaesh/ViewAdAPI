using Microsoft.AspNetCore.Mvc;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.Filters;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ViewAdAPI.Controllers
{
    /// <summary>
    /// CRUD operations for Transactions table
    /// </summary>
    /// <response code="400">Returns error messages if request fails</response>
    [Authorize]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsBAL _transactionsBAL;

        public TransactionsController(ITransactionsBAL transactionsBAL)
        {
            _transactionsBAL = transactionsBAL;
        }
        // GET: api/transactions
        /// <summary>
        /// To get all transactions with paginated
        /// </summary>
        /// <response code="200">It will return list of transactions with pagination</response>
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResponse<IEnumerable<TransactionResponse>>>> GetTransactions(PaginationRequest request, Guid? userId = null, Guid? tokenId = null)
        {
            try
            {
                return await _transactionsBAL.GetAllAsync(request, userId, tokenId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // GET api/transactions/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To get a transaction by id
        /// </summary>
        /// <response code="200">It will return single transaction object</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionResponse>> GetTransaction(Guid id)
        {
            try
            {
                return await _transactionsBAL.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // POST api/transactions
        /// <summary>
        /// To create a transaction
        /// </summary>
        /// <response code="201">It will return transaction id(Guid) which is created</response>
        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateTransaction([FromBody] CreateTransaction request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var transactionId = await _transactionsBAL.InsertAsync(new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Amount = request.Amount,
                    DateTime = request.DateTime,
                    TokenId = request.TokenId,
                    WithdrawDateTime = request.WithdrawDateTime
                });
                if (transactionId is null)
                    return BadRequest(new { error = "Unable to create Transaction" });
                return CreatedAtAction(nameof(GetTransaction), new { id = transactionId }, transactionId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }

        // PUT api/transactions/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To update a transaction
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTransaction(Guid id, [FromBody] CreateTransaction request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _transactionsBAL.UpdateAsync(new Transaction
                {
                    Id = id,
                    UserId = request.UserId,
                    Amount = request.Amount,
                    DateTime = request.DateTime,
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

        // DELETE api/transactions/b4e0c950-d1f6-41a0-9ed0-0601facb5124
        /// <summary>
        /// To delete a transaction
        /// </summary>
        /// <response code="200">It will return true if it is deleted successfully</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> DeleteTransaction(Guid id)
        {
            try
            {
                return await _transactionsBAL.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"{ex.Message} --> {ex.StackTrace}" });
            }
        }
    }
}

