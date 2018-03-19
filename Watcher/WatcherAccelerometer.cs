using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardsTech.Sensors;

namespace Watcher
{
    public class WatcherAccelerometer
    {
        //Values are in radians/sec
        public int G { get; set; }
        public int FieldStrength { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public WatcherAccelerometer(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public WatcherAccelerometer(int g)
        {
            G = g;
        }
    }
}
