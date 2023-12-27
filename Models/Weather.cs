using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WeatherSoapApi.Models
{
    [Serializable]
    public class Weather
    {
        public string City { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;

        [XmlElement("Temperature")]
        public TemperatureData TemperatureData { get; set; }

        [XmlIgnore]
        public string TemperatureType
        {
            get { return TemperatureData?.TemperatureType; }
            set
            {
                if (TemperatureData == null)
                    TemperatureData = new TemperatureData();

                TemperatureData.TemperatureType = value;
            }
        }

        [XmlIgnore]
        public string Temperature
        {
            get { return TemperatureData?.Temperature; }
            set
            {
                if (TemperatureData == null)
                    TemperatureData = new TemperatureData();

                TemperatureData.Temperature = value;
            }
        }
    }

    [Serializable]
    public class TemperatureData
    {
        [XmlText]
        public string Temperature { get; set; } = string.Empty;

        [XmlAttribute("type")]
        public string TemperatureType { get; set; } = string.Empty;
    }
}