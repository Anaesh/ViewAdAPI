using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL.Interface
{
    public interface ITransactionsBAL
    {
        Task<Guid?> InsertAsync(Transaction request);
        Task<TransactionResponse> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<TransactionResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId);
        Task UpdateAsync(Transaction request);
        Task<bool> DeleteAsync(Guid id);
    }
}

