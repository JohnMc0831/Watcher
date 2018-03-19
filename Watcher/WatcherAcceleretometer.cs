using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watcher
{
    class WatcherAccelerotometer
    {
        //G = 1 Earth Gravity
        public int G { get; set; }

        public WatcherAccelerotometer(int g)
        {
            G = g;
        }
    }
}
