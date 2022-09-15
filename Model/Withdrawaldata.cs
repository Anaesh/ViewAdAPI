using System;
namespace ViewAdAPI.Model
{
    public class Withdrawaldata
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public string? Link { get; set; }
        public string? RequestStatus { get; set; }
        public Guid TokenId { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
}

