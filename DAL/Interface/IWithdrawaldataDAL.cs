using System;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL.Interface
{
    public interface IWithdrawaldataDAL
    {
        Task<Guid?> InsertAsync(Withdrawaldata request);
        Task<WithdrawaldataResponse> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<WithdrawaldataResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId);
        Task UpdateAsync(Withdrawaldata request);
        Task<bool> DeleteAsync(Guid id);
    }
}

