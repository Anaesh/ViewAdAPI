using System;
using System.ComponentModel.DataAnnotations;

namespace ViewAdAPI.Model.DTOs
{
    public class CreateUser
    {
        public int Coins { get; set; }

        [StringLength(2000, ErrorMessage ="Max string length is 2000")]
        public string? DeviceToken { get; set; }

        [StringLength(500, ErrorMessage = "Max string length is 500")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage ="Email is not valid")]
        public string? Email { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 255")]
        public string? FirstName { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 255")]
        public string? LastName { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 255")]
        public string? Password { get; set; }

        [Required]
        [RegularExpression("^\\+?[1-9][0-9]{7,14}$", ErrorMessage = "Phonenumber is not valid")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phonenumber should be 10 digits")]
        public string? PhoneNumber { get; set; }
    }
}

