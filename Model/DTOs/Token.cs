using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViewAdAPI.Model.DTOs
{
    public class CreateToken
    {
        public int CurrentRewardValue { get; set; }

        [StringLength(2000)]
        public string? Image { get; set; }

        public bool IsActive { get; set; }

        public int MinimumWithdrawl { get; set; }

        [StringLength(255)]
        public string? Symbol { get; set; }

        public int TokenId { get; set; }

        [StringLength(255)]
        public string? TokenName { get; set; }
    }
}

