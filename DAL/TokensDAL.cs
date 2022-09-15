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
    public class TokensDAL : ITokensDAL
    {
        private readonly IConfiguration _config;

        public TokensDAL(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = @"
                        DELETE FROM Tokens
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

        public async Task<PaginatedResponse<IEnumerable<Token>>> GetAllAsync(PaginationRequest request)
        {
            var query = @"
                        SELECT COUNT(Id) FROM Tokens;

                        SELECT
                            Id, CurrentRewardValue, Image, IsActive, MinimumWithdrawl, Symbol, TokenId, TokenName
                        FROM
                            Tokens
                        LIMIT @Take
                        OFFSET @Skip;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                var reader = await connection.QueryMultipleAsync(query, new { Take = request.GetPageSize(), Skip = request.Skip() });

                var totalCount = reader.Read<int>().FirstOrDefault();
                var data = reader.Read<Token>();

                return new PaginatedResponse<IEnumerable<Token>>(totalCount, data, request.GetPageNumber(), request.GetPageSize());
            }
        }

        public async Task<Token> GetAsync(Guid id)
        {
            var query = @"
                        SELECT
                            Id, CurrentRewardValue, Image, IsActive, MinimumWithdrawl, Symbol, TokenId, TokenName
                        FROM
                            Tokens
                        WHERE Id = @Id;
                        ";
            using (var connection = new MySqlConnection(_config.GetConnectionString("Default")))
            {
                SqlMapper.AddTypeHandler(new GuidTypeHandler());
                return await connection.QueryFirstOrDefaultAsync<Token>(query, new { Id = id });
            }
        }

        public async Task<Guid?> InsertAsync(Token request)
        {
            var query = @"
                        INSERT INTO Tokens
                            (Id, CurrentRewardValue, Image, IsActive, MinimumWithdrawl, Symbol, TokenId, TokenName)
                        VALUES
                            (@Id, @CurrentRewardValue, @Image, @IsActive, @MinimumWithdrawl, @Symbol, @TokenId, @TokenName)
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

        public async Task UpdateAsync(Token request)
        {
            var query = @"
                        UPDATE Tokens
                        SET
                            CurrentRewardValue = @CurrentRewardValue,
                            Image = @Image,
                            IsActive = @IsActive,
                            MinimumWithdrawl = @MinimumWithdrawl,
                            Symbol = @Symbol,
                            TokenId = @TokenId,
                            TokenName = @TokenName
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

