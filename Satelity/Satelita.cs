using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satelity
{
    public class Satelita
    {
        public string name { get; set; }
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double velocity { get; set; }
        public string visibility { get; set; }
        public double footprint { get; set; }
        public int timestamp { get; set; }
        public double daynum { get; set; }
        public double solar_lat { get; set; }
        public double solar_lon { get; set; }
        public string units { get; set; }

        public string Show(int flag)
        {
            if (flag == 1)
            {
                return Time(timestamp).ToString() + "\t"+ lat() + "°\n";
            }
            else if (flag == 2)
            {
                return Time(timestamp).ToString() + "\t" + lon() + "°\n";
            }
            else if (flag == 3)
            {
                return Time(timestamp).ToString() + "\t" + npm() + "  km\n";
            }
            else if (flag == 4)
            {
                return Time(timestamp).ToString() + "\t" + vel() + "  km/h\n";
            }
            return null;
        }

        public string lat()
        {
            return latitude.ToString();
        }
        public string lon()
        {
            return longitude.ToString();
        }
        public string npm()
        {
            return altitude.ToString();
        }
        public string vel()
        {
            return velocity.ToString();
        }

        public static DateTime Time(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }




}
