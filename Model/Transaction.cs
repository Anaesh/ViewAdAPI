using System;
using System.Text.Json.Serialization;

namespace ViewAdAPI.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid TokenId { get; set; }
        public DateTime WithdrawDateTime { get; set; }
    }
}

