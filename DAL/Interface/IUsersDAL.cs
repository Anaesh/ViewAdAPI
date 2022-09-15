using System;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL.Interface
{
    public interface IUsersDAL
    {
        Task<Guid?> InsertAsync(User request);
        Task<User> GetAsync(Guid id);
        Task<PaginatedResponse<IEnumerable<User>>> GetAllAsync(PaginationRequest request);
        Task UpdateAsync(User request);
        Task<bool> DeleteAsync(Guid id);
    }
}

