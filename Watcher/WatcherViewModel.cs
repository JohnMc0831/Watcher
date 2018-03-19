using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardsTech.Sensors;

namespace Watcher
{
    /// This class provide the VM for the Watcher main UI.  At present, this includes the following elements:
    /// 1.  Temperature
    /// 2.  Barometric Pressure
    /// 3.  Humidity
    /// 4.  Compass/GPS Coords/Magnetometer
    /// 5.  Acceleroemter
    /// 6.  LED
    public class WatcherViewModel
    {
        public WatcherTemperature Temperature { get; set; }
        public WatcherPressure BarometricPressure { get; set; }
        public WatcherHumidity Humidity { get; set; }
        public WatcherAccelerometer Accelerometer { get; set; }
        public WatcherPose Pose { get; set; }
        public WatcherGyro Gyroscope { get; set; }
        public WatcherMagnetometer Magnetometer { get; set; }

        public WatcherViewModel()
        {
            
        }
    }
}
