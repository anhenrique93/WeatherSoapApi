using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Services;
using WeatherSoapApi.Models;
using WeatherSoapApi.Repositories.Services.Interfaces;

namespace WeatherSoapApi.Services
{
    /// <summary>
    /// SOAP API that returns the latitude, longitude, and temperature of a city in Celsius, requiring only the input of the city name.
    /// </summary>
    [WebService(Namespace = "http://anhenrique.netlify.app/",
        Description = "SOAP API that returns the latitude, longitude, and temperature of a city in Celsius, requiring only the input of the city name.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeatherService : System.Web.Services.WebService
    {
        private readonly IWeatherService _weatherService;
        private readonly IGeocodeService _geocodeService;

        public WeatherService()
        {
            var httpClient = new HttpClient(); // Configure conforme necessário
            _weatherService = DependencyConfig.ConfigureWeatherService(httpClient);
            _geocodeService = DependencyConfig.ConfigureGeocodeService(httpClient);
        }

        /// <summary>
        /// returns the latitude, longitude, and temperature of a city in Celsius 
        /// </summary>
        /// <param name="city"></param>
        /// <returns>Weather</returns>
        [WebMethod]
        public Weather GetWeather(string city)
        {
            Geocode geocode = null;
            Weather weather = null;

            try
            {
                // Inicia uma nova thread para executar o método assíncrono
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        geocode = _geocodeService.GetCityGeoCode(city).Result;

                        try
                        {
                            weather = _weatherService.GetWeather(geocode.Latitude, geocode.Longitude).Result;
                            weather.City = geocode.CityName;
                        }
                        catch (Exception ex)
                        {
                            Context.Response.Write(ex.Message);
                        }
                    }
                    catch (Exception ex) 
                    {
                        Context.Response.Write(ex.Message);
                    }

                }, TaskCreationOptions.LongRunning)
                .Wait(); 

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting weather information: {ex.Message}");
                Context.Response.Write("Error getting weather information.");
            }
            return weather;
        }
    }
}
