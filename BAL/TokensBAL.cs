using System;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL
{
    public class TokensBAL: ITokensBAL
    {
        private readonly ITokensDAL _tokensDAL;

        public TokensBAL(ITokensDAL tokensDAL)
        {
            _tokensDAL = tokensDAL;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _tokensDAL.DeleteAsync(id);
        }

        public Task<PaginatedResponse<IEnumerable<Token>>> GetAllAsync(PaginationRequest request)
        {
            return _tokensDAL.GetAllAsync(request);
        }

        public Task<Token> GetAsync(Guid id)
        {
            return _tokensDAL.GetAsync(id);
        }

        public Task<Guid?> InsertAsync(Token request)
        {
            return _tokensDAL.InsertAsync(request);
        }

        public Task UpdateAsync(Token request)
        {
            return _tokensDAL.UpdateAsync(request);
        }
    }
}

