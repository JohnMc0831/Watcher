using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichardsTech.Sensors;

namespace Watcher
{
    public class WatcherMagnetometer
    {
        public int FieldStrength { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public WatcherMagnetometer(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public WatcherMagnetometer(int fieldStrength)
        {
            FieldStrength = fieldStrength;
        }
    }
}
