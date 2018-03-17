using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace Watcher
{
    /// <summary>
    /// The WatcherTemperature class is the ViewModel component which stores and processes temparature-related
    /// telemetry.  It should be noted that the class stores all temperatures in celsius only and merely converts
    /// to fahrenheit for display purposes.  EMBRACE THE METRIC WAVE!
    /// </summary>
    public class WatcherTemperature
    {
        public static double Celsius { get; set; }
        public static double LastCelsius { get; set; }
        public static bool IsDropping { get; set; }
        public static bool IsRising { get; set; }
        public static bool IsSteady { get; set; }

        public WatcherTemperature()
        {

        }

        public WatcherTemperature(double temp)
        {
           SetTemperature(temp);
        }
        /// <summary>
        /// SetTemperature(double temp) - this method updates the current temperature being stored here
        /// Again, all temps should be passed as celsius!  Along with setting the Celsius property to the
        /// current temperature, it updates the LastCelsius property with any prior stored Celsius (double)
        /// value and flips the booleans for:
        ///     -IsRising - the new temperature is higher than the stored temp
        ///     -IsDropping - the new temperature is lower thant he stored temp
        ///     -IsSteady - there is no change in temperature
        /// </summary>
        /// <param name="temp"></param>
        public void SetTemperature(double temp)
        {
            LastCelsius = Math.Abs(Celsius) > Double.Epsilon ? Celsius : 0.0;
            Celsius = temp;
            if (LastCelsius < Celsius)
            {
                //temp is rising
                IsRising = true;
                IsDropping = false;
                IsSteady = false;

            }
            else if (LastCelsius > Celsius)
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

        public static double ToFahrenheit()
        {
            return ((9.0 / 5.0) * Celsius) + 32;
        }

        public static double ToCelsius(double f)
        {
            return (5.0 / 9.0) * (f - 32);
        }
    }
}
