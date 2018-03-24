using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    public class WatcherPressure
    {
        public static float Millibars { get; set; }
        public static float LastMillibars { get; set; }
        public static bool IsDropping { get; set; }
        public static bool IsRising { get; set; }
        public static bool IsSteady { get; set; }

        public WatcherPressure()
        {

        }

        public WatcherPressure(float millibars)
        {
            SetPressure(millibars);
        }

        public void SetPressure(float millibars)
        {
            LastMillibars = Math.Abs(Millibars) > 0f ? Millibars : 0f;
            Millibars = millibars;
            if (LastMillibars < Millibars)
            {
                //temp is rising
                IsRising = true;
                IsDropping = false;
                IsSteady = false;

            }
            else if (LastMillibars > Millibars)
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
