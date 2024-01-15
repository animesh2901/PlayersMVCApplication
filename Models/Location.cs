namespace PlayersMVCApplication.Models
{
    public class Location
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }

        public List<State>? States { get; set; }

        public class State
        {
            public int StateID { get; set;}
            public string? StateName { get; set;}
            public string? StateCode { get; set;}

            public List<City>? Cities { get; set; }

            public class City
            {
                public int CityID { get; set;}
                public string? CityName { get; set;}
                public string? CityCode { get; set;}

                public List<Zipcode>? ZipCodes { get; set; }

                public class Zipcode
                {
                    public int ZipcodeID { get; set; }
                    public int ZipcodeNumber { get; set; }
                }
            }
        }
    }
}
