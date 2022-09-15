using System;
using System.ComponentModel.DataAnnotations;

namespace ViewAdAPI.Model.DTOs
{
    public class WithdrawaldataResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public string? Link { get; set; }
        public string? RequestStatus { get; set; }
        public BaseToken? Token { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
    public class CreateWithdrawaldata
    {
        public Guid UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount value should be greater than 0")]
        public int Amount { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 2000")]
        public string? Link { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 200")]
        public string? RequestStatus { get; set; }

        public Guid TokenId { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
}

