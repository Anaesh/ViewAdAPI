using System;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL
{
    public class WithdrawaldataBAL: IWithdrawaldataBAL
    {
        private readonly IWithdrawaldataDAL _withdrawaldataDAL;

        public WithdrawaldataBAL(IWithdrawaldataDAL withdrawaldataDAL)
        {
            _withdrawaldataDAL = withdrawaldataDAL;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _withdrawaldataDAL.DeleteAsync(id);
        }

        public Task<PaginatedResponse<IEnumerable<WithdrawaldataResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId)
        {
            return _withdrawaldataDAL.GetAllAsync(request, userId, tokenId);
        }

        public Task<WithdrawaldataResponse> GetAsync(Guid id)
        {
            return _withdrawaldataDAL.GetAsync(id);
        }

        public Task<Guid?> InsertAsync(Withdrawaldata request)
        {
            return _withdrawaldataDAL.InsertAsync(request);
        }

        public Task UpdateAsync(Withdrawaldata request)
        {
            return _withdrawaldataDAL.UpdateAsync(request);
        }
    }
}

