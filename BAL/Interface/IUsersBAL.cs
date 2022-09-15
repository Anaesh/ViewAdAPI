using System;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL.Interface
{
    public interface IUsersBAL
    {
        Task<Guid?> InsertAsync(User request);
        Task<User> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<User>>> GetAllAsync(PaginationRequest request);
        Task UpdateAsync(User request);
        Task<bool> DeleteAsync(Guid id);
    }
}

