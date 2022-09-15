using System;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL.Interface
{
    public interface ITokensDAL
    {
        Task<Guid?> InsertAsync(Token request);
        Task<Token> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<Token>>> GetAllAsync(PaginationRequest request);
        Task UpdateAsync(Token request);
        Task<bool> DeleteAsync(Guid id);
    }
}

