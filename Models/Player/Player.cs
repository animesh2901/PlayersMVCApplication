using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlayersMVCApplication.Models.Player
{
    public class Player
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="REQUIRED")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        public string? Team { get; set; }
        [Required(ErrorMessage = "REQUIRED")]
        public int JersyNumber { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
    public class Country
    {
        //public int Id { get; set; }
        public string? country_name { get; set; }
        public string? country_short_name { get; set; }
        public string? country_phone_code { get; set; }
    }
    public class State
    {
        //public int Id { get; set; }
        public string? state_name { get; set; }
    }
    public class City
    {
        //public int Id { get; set; }
        public string? city_name { get; set; }
    }
}
