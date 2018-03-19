using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardsTech.Sensors;

namespace Watcher
{
    public class WatcherGyro
    {
        //Values are in radians/sec
        public int RadiansPerSecond { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public WatcherGyro(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public WatcherGyro(int rads)
        {
            RadiansPerSecond = rads;
        }
    }
}
