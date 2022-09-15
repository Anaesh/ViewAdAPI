using System;
using Dapper;
using MySql.Data.MySqlClient;
using ViewAdAPI.DAL.Interface;
using ViewAdAPI.DAL.TypeHandlers;
using ViewAdAPI.Model;
using ViewAdAPI.Model.DTOs;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ViewAdAPI.DAL
{
    public class TransactionsDAL : ITransactionsDAL
    {
        private readonly IConfiguration _config;

        public TransactionsDAL(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = @"
                        DELETE FROM Transactions
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

        public async Task<PaginatedResponse<IEnumerable<TransactionResponse>>> GetAllAsync(PaginationRequest request, Guid? userId, Guid? tokenId)
        {
            var whereQuery = "";
            if (userId.HasValue || tokenId.HasValue)
            {
                if (userId.HasValue)
                    whereQuery = " WHERE T.UserId = @UserId";
                if (tokenId.HasValue)
                    whereQuery += string.IsNullOrEmpty(whereQuery) ? " WHERE T.TokenId = @TokenId " : " AND T.TokenId = @TokenId ";
            }
            var query = @$"
                        SELECT COUNT(T.Id) FROM Transactions T{whereQuery};

                        SELECT
                            T.Id, T.UserId, T.Amount, T.`DateTime`, T.WithdrawDateTime,
                            JSON_OBJECT('Id', TK.Id, 'TokenId', TK.TokenId, 'TokenName', TK.TokenName, 'MinimumWithdrawl', TK.MinimumWithdrawl) AS Token
                        FROM
                            Transactions T
                        JOIN Tokens TK ON TK.Id = T.TokenId
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
                var data = reader.Read<TransactionResponse>();

                return new PaginatedResponse<IEnumerable<TransactionResponse>>(totalCount, data, request.GetPageNumber(), request.GetPageSize());
            }
        }

        public async Task<TransactionResponse> GetAsync(Guid id)
        {
            var query = @"
                        SELECT
                            T.Id, T.UserId, T.Amount, T.`DateTime`, T.WithdrawDateTime,
                            JSON_OBJECT('Id', TK.Id, 'TokenId', TK.TokenId, 'TokenName', TK.TokenName, 'MinimumWithdrawl', TK.MinimumWithdrawl) AS Token
                        FROM
                            Transactions T
                        JOIN Tokens TK ON TK.Id = T.TokenId
                        WHERE T.Id = @Id;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                SqlMapper.AddTypeHandler(new CustomObjectTypeHandler<BaseToken>());
                return await connection.QueryFirstOrDefaultAsync<TransactionResponse>(query, new { Id = id });
            }
        }

        public async Task<Guid?> InsertAsync(Transaction request)
        {
            var query = @"
                        INSERT INTO Transactions
                            (Id, UserId, Amount, `DateTime`, TokenId, WithdrawDateTime)
                        VALUES
                            (@Id, @UserId, @Amount, @DateTime, @TokenId, @WithdrawDateTime)
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

        public async Task UpdateAsync(Transaction request)
        {
            var query = @"
                        UPDATE Transactions
                        SET
                            UserId = @UserId,
                            Amount = @Amount,
                            DateTime = @DateTime,
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

