using System;
using System.Text.Json.Serialization;

namespace ViewAdAPI.Model
{
    public class Token: BaseToken
    {
        public int CurrentRewardValue { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public string? Symbol { get; set; }
    }

    public class BaseToken
    {
        public Guid Id { get; set; }
        public int TokenId { get; set; }
        public string? TokenName { get; set; }
        public int MinimumWithdrawl { get; set; }
    }
}

