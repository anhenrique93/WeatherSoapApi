using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherSoapApi.Models;
using static System.Net.WebRequestMethods;

namespace WeatherSoapApi.Repositories.Services.Interfaces
{
    public class GeocodeService : IGeocodeService
    {
        private readonly HttpClient _client;
        private readonly String _apiKey = "";

        public GeocodeService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Geocode> GetCityGeoCode(string city)
        {
            try
            {
                var geocode = new Geocode();

                var connectionString = $"https://geocode.maps.co/search?q={city}&api_key={_apiKey}";

                var response = await _client.GetAsync(connectionString);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<dynamic>>(responseString);

                    if (result != null && result.Count > 0)
                    {
                        var firstItem = result[0];

                        geocode.CityName = firstItem.display_name;
                        geocode.Longitude = firstItem.lon;
                        geocode.Latitude = firstItem.lat;
                    }
                }
                else
                {
                    Debug.WriteLine("Error on get!");
                }

                return geocode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
