using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WeatherSoapApi.Repositories.Interfaces;
using WeatherSoapApi.Repositories.Services.Interfaces;

namespace WeatherSoapApi
{
    public static class DependencyConfig
    {
        public static IWeatherService ConfigureWeatherService(HttpClient httpClient)
        {
            return new WeatherService(httpClient);
        }

        public static IGeocodeService ConfigureGeocodeService(HttpClient httpClient)
        {
            return new GeocodeService(httpClient);
        }
    }
}