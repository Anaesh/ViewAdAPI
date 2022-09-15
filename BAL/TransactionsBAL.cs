using System;
using ViewAdAPI.Model;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL
{
    public class TransactionsBAL: ITransactionsBAL
    {
        private readonly ITransactionsDAL _transactionsDAL;

        public TransactionsBAL(ITransactionsDAL transactionsDAL)
        {
            _transactionsDAL = transactionsDAL;
        }


        public Task<bool> DeleteAsync(Guid id)
        {
            return _transactionsDAL.DeleteAsync(id);
        }

        public Task<PaginatedResponse<IEnumerable<TransactionResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId)
        {
            return _transactionsDAL.GetAllAsync(request, userId, tokenId);
        }

        public Task<TransactionResponse> GetAsync(Guid id)
        {
            return _transactionsDAL.GetAsync(id);
        }

        public Task<Guid?> InsertAsync(Transaction request)
        {
            return _transactionsDAL.InsertAsync(request);
        }

        public Task UpdateAsync(Transaction request)
        {
            return _transactionsDAL.UpdateAsync(request);
        }
    }
}

