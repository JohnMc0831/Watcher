using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi;
using Emmellsoft.IoT.Rpi.SenseHat;
using RichardsTech.Sensors;

namespace Watcher
{
    public class WatcherPose
    {
        //Consists of roll=X, pitch=Y and yaw=Z
        //All values are in Radians

        public double? X { get; set; }
        public double? Roll { get; set; }
        public double? Y { get; set; }
        public double? Pitch { get; set; }
        public double? Z { get; set; }
        public double? Yaw { get; set; }

        public WatcherPose(Vector3 vector)
        {
            X = vector.X;
            Roll = X;
            Y = vector.Y;
            Pitch = Y;
            Z = vector.Z;
            Yaw = Z;
        }

        public double ToDegrees(int val)
        {
            double degrees = val * 57.2958;
            return degrees;
        }
    }
}
