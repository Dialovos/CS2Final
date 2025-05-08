using Newtonsoft.Json;
using System.Collections.Generic;

namespace CS2_Final
{
    /// <summary>
    /// detailed weather condition info
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// main weather  
        /// </summary>
        [JsonProperty("main")]
        public string Main { get; set; }

        /// <summary>
        /// weather condition description 
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// icon ID
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    /// <summary>
    /// main weather parameters
    /// </summary>
    public class MainWeatherData
    {
        /// <summary>
        /// Current temperature.
        /// </summary>
        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// temp that it feels like
        /// </summary>
        [JsonProperty("feels_like")]
        public double weatherFeelsLike { get; set; }

        /// <summary>
        /// humidity
        /// </summary>
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    /// <summary>
    /// wind speed information
    /// </summary>
    public class WindData
    {
        /// <summary>
        /// wind speed
        /// </summary>
        [JsonProperty("speed")]
        public double windSpeed { get; set; }
    }

    /// <summary>
    /// root object
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// list of weather conditions
        /// </summary>
        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; }

        /// <summary>
        /// weather
        /// </summary>
        [JsonProperty("main")]
        public MainWeatherData Main { get; set; }

        /// <summary>
        /// wind info
        /// </summary>
        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        /// <summary>
        /// city name as returned by the api
        /// </summary>
        [JsonProperty("name")]
        public string cityName { get; set; } // name as provided by the person
    }
}