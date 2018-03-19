using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    public class WatcherHumidity
    {
        public static double? Humidity { get; set; }
        public static double? LastHumidity { get; set; }
        public static bool IsDropping { get; set; }
        public static bool IsRising { get; set; }
        public static bool IsSteady { get; set; }

        public WatcherHumidity()
        {

        }

        public WatcherHumidity(double? humidity = 0)
        {
            
        }

        public void SetHumidity(double? humidity)
        {
            LastHumidity = Humidity > 0 ? Humidity : 0;
            Humidity = humidity;
            if (LastHumidity < Humidity)
            {
                //temp is rising
                IsRising = true;
                IsDropping = false;
                IsSteady = false;

            }
            else if (LastHumidity > Humidity)
            {
                //dropping
                IsRising = false;
                IsDropping = true;
                IsSteady = false;
            }
            else
            {
                //steady/no change
                IsRising = false;
                IsDropping = false;
                IsSteady = true;
            }
        }
    }
}
