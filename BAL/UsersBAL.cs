using System;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.BAL
{
    public class UsersBAL : IUsersBAL
    {
        private readonly IUsersDAL _usersDAL;

        public UsersBAL(IUsersDAL usersDAL)
        {
            _usersDAL = usersDAL;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _usersDAL.DeleteAsync(id);
        }

        public Task<PaginatedResponse<IEnumerable<User>>> GetAllAsync(PaginationRequest request)
        {
            return _usersDAL.GetAllAsync(request);
        }

        public Task<User> GetAsync(Guid id)
        {
            return _usersDAL.GetAsync(id);
        }

        public Task<Guid?> InsertAsync(User request)
        {
            return _usersDAL.InsertAsync(request);
        }

        public Task UpdateAsync(User request)
        {
            return _usersDAL.UpdateAsync(request);
        }
    }
}

