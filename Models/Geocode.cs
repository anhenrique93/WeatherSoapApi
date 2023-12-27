using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherSoapApi.Models
{
    [Serializable]
    public class Geocode
    {
        public string CityName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = String.Empty;
    }
}