using System;
using Dapper;
using MySql.Data.MySqlClient;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.DAL.TypeHandlers;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;

namespace ViewAdAPI.DAL
{
    public class WithdrawaldataDAL : IWithdrawaldataDAL
    {
        private readonly IConfiguration _config;

        public WithdrawaldataDAL(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = @"
                        DELETE FROM Withdrawaldata
                        WHERE
                            Id = @Id
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                var affectRows = await connection.ExecuteAsync(query, new { Id = id });
                if (affectRows > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<PaginatedResponse<IEnumerable<WithdrawaldataResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId)
        {
            var whereQuery = "";
            if (userId.HasValue || tokenId.HasValue)
            {
                if (userId.HasValue)
                    whereQuery = " WHERE W.UserId = @UserId";
                if (tokenId.HasValue)
                    whereQuery += string.IsNullOrEmpty(whereQuery) ? " WHERE W.TokenId = @TokenId " : " AND W.TokenId = @TokenId ";
            }
            var query = @$"
                        SELECT COUNT(W.Id) FROM Withdrawaldata W{whereQuery};

                        SELECT
                            W.Id, W.UserId, W.Amount, W.Link, W.RequestStatus, W.WithdrawDateTime,
                            JSON_OBJECT('Id', TK.Id, 'TokenId', TK.TokenId, 'TokenName', TK.TokenName, 'MinimumWithdrawl', TK.MinimumWithdrawl) AS Token
                        FROM
                            Withdrawaldata W
                        JOIN Tokens TK ON TK.Id = W.TokenId
                        {whereQuery}
                        LIMIT @Take
                        OFFSET @Skip;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                SqlMapper.AddTypeHandler(new CustomObjectTypeHandler<BaseToken>());
                var reader = await connection.QueryMultipleAsync(query, new
                {
                    UserId = userId,
                    TokenId = tokenId,
                    Take = request.GetPageSize(),
                    Skip = request.Skip()
                });

                var totalCount = reader.Read<int>().FirstOrDefault();
                var data = reader.Read<WithdrawaldataResponse>();

                return new PaginatedResponse<IEnumerable<WithdrawaldataResponse>>(totalCount, data, request.GetPageNumber(), request.GetPageSize());
            }
        }

        public async Task<WithdrawaldataResponse> GetAsync(Guid id)
        {
            var query = @"
                        SELECT
                            W.Id, W.UserId, W.Amount, W.Link, W.RequestStatus, W.WithdrawDateTime,
                            JSON_OBJECT('Id', TK.Id, 'TokenId', TK.TokenId, 'TokenName', TK.TokenName, 'MinimumWithdrawl', TK.MinimumWithdrawl) AS Token
                        FROM
                            Withdrawaldata W
                        JOIN Tokens TK ON TK.Id = W.TokenId
                        WHERE W.Id = @Id;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                SqlMapper.AddTypeHandler(new CustomObjectTypeHandler<BaseToken>());
                return await connection.QueryFirstOrDefaultAsync<WithdrawaldataResponse>(query, new { Id = id });
            }
        }

        public async Task<Guid?> InsertAsync(Withdrawaldata request)
        {
            var query = @"
                        INSERT INTO Withdrawaldata
                            (Id, UserId, Amount, Link, RequestStatus, TokenId, WithdrawDateTime)
                        VALUES
                            (@Id, @UserId, @Amount, @Link, RequestStatus, @TokenId, @WithdrawDateTime)
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

        public async Task UpdateAsync(Withdrawaldata request)
        {
            var query = @"
                        UPDATE Withdrawaldata
                        SET
                            UserId = @UserId,
                            Amount = @Amount,
                            Link = @Link,
                            RequestStatus = @RequestStatus,
                            TokenId = @TokenId,
                            WithdrawDateTime = @WithdrawDateTime
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

