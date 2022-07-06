using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot
{
    public class Coord
    {
        public double lon;
        public double lat;
    }
    public class Weather
    {
        public string main;
        public string description;
    }
    public class Main
    {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public double pressure;
        public double humidity;
    }
    public class Wind
    {
        public double speed;
        public double deg;
        public double gust;
    }
    public class Sys
    {
        public string country;
        public double sunrise;
        public double sunset;
    }
    public class WeatherResponse
    {
        public string name;
        public Coord coord;
        public Weather[] weather;
        public Main main;
        public int visibility;
        public Wind wind;
        public int dt;
        public Sys sys;
        public double timezone;
    }
}
