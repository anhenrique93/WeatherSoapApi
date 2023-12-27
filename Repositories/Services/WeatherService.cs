using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherSoapApi.Models;
using WeatherSoapApi.Repositories.Services.Interfaces;

namespace WeatherSoapApi.Repositories.Interfaces
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;

        public WeatherService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Weather> GetWeather(string latitude, string longitude)
        {
            try
            {
                Weather weather = new Weather();

                var connectionString = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

                var response = await _client.GetAsync(connectionString);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<dynamic>(responseString);

                    if (result != null)
                    {
                        weather.Latitude = result.latitude;
                        weather.Longitude = result.longitude;
                        weather.TemperatureType = result.current_weather_units.temperature;
                        weather.Temperature = result.current_weather.temperature;
                    }
                }

                return weather;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}