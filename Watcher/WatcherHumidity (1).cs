using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    public class WatcherHumidity
    {
        public static float Humidity { get; set; }
        public static float LastHumidity { get; set; }
        public static bool IsDropping { get; set; }
        public static bool IsRising { get; set; }
        public static bool IsSteady { get; set; }

        public WatcherHumidity()
        {

        }

        public WatcherHumidity(float humidity)
        {
            SetHumidity(humidity);
        }

        public void SetHumidity(float humidity)
        {
            LastHumidity = Math.Abs(Humidity) > 0f ? Humidity : 0f;
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
