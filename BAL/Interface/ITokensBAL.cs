using System;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL.Interface
{
    public interface ITokensBAL
    {
        Task<Guid?> InsertAsync(Token request);
        Task<Token> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<Token>>> GetAllAsync(PaginationRequest request);
        Task UpdateAsync(Token request);
        Task<bool> DeleteAsync(Guid id);
    }
}

