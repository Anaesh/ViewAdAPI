using System;
using Dapper;
using MySql.Data.MySqlClient;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.DAL.TypeHandlers;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL
{
    public class UsersDAL : IUsersDAL
    {
        private readonly IConfiguration _config;

        public UsersDAL(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = @"
                        DELETE FROM Users
                        WHERE
                            Id = @Id
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                var affectRows = await connection.ExecuteAsync(query, new { Id = id});
                if (affectRows > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<PaginatedResponse<IEnumerable<User>>> GetAllAsync(PaginationRequest request)
        {
            var query = @"
                        SELECT COUNT(Id) FROM Users;

                        SELECT
                            Id, Coins, DeviceToken, Email, FirstName, LastName, Password, PhoneNumber
                        FROM
                            Users
                        LIMIT @Take
                        OFFSET @Skip;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                var reader = await connection.QueryMultipleAsync(query, new { Take = request.GetPageSize(), Skip = request.Skip() });
                var totalCount = reader.Read<int>().FirstOrDefault();
                var data = reader.Read<User>();

                return new PaginatedResponse<IEnumerable<User>>(totalCount, data, request.GetPageNumber(), request.GetPageSize());
            }
        }

        public async Task<User> GetAsync(Guid id)
        {
            var query = @"
                        SELECT
                            Id, Coins, DeviceToken, Email, FirstName, LastName, Password, PhoneNumber
                        FROM
                            Users
                        WHERE Id = @Id;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
            }
        }

        public async Task<Guid?> InsertAsync(User request)
        {
            var query = @"
                        INSERT INTO Users
                            (Id, Coins, DeviceToken, Email, FirstName, LastName, Password, PhoneNumber)
                        VALUES
                            (@Id, @Coins, @DeviceToken, @Email, @FirstName, @LastName, @Password, @PhoneNumber)
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                var affectRows = await connection.ExecuteAsync(query, request);
                if (affectRows > 0)
                {
                    return request.Id;
                }
            }
            return null;
        }

        public async Task UpdateAsync(User request)
        {
            var query = @"
                        UPDATE Users
                        SET
                            Coins = @Coins,
                            DeviceToken = @DeviceToken,
                            Email = @Email,
                            FirstName = @FirstName,
                            LastName = @LastName,
                            Password = @Password,
                            PhoneNumber = @PhoneNumber
                        WHERE
                            Id = @Id
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                await connection.ExecuteAsync(query, request);
            }
        }
    }
}

