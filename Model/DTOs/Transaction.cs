using System.ComponentModel.DataAnnotations;
namespace ViewAdAPI.Model.DTOs
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
        public BaseToken? Token { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
    public class CreateTransaction
    {
        public Guid UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Amount value should be greater than 0")]
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid TokenId { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
}

