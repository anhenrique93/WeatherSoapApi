using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherSoapApi.Models;

namespace WeatherSoapApi.Repositories.Services.Interfaces
{
    public interface IGeocodeService
    {
        Task<Geocode> GetCityGeoCode(string city);
    }
}
