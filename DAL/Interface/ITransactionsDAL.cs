using System;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL.Interface
{
    public interface ITransactionsDAL
    {
        Task<Guid?> InsertAsync(Transaction request);
        Task<TransactionResponse> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<TransactionResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId);
        Task UpdateAsync(Transaction request);
        Task<bool> DeleteAsync(Guid id);
    }
}

