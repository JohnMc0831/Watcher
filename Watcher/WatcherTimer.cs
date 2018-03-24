using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace Watcher
{
    public class WatcherTimer
    {
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 10;
        private string TimerLog = string.Empty;
        private ISenseHatSensors sensors;

        public WatcherTimer(ISenseHatSensors Sensors)
        {
            sensors = Sensors;
            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            //IsEnabled defaults to false
            TimerLog += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            TimerLog += "Calling dispatcherTimer.Start()\n";
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
            TimerLog += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
            Debug.WriteLine(TimerLog);
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            TimerLog += timesTicked + "\t time since last tick: " + span.ToString() + "\n";
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                stopTime = time;
                TimerLog += "Calling dispatcherTimer.Stop()\n";
                dispatcherTimer.Stop();
                                //update the UI
                //IsEnabled should now be false after calling stop
                TimerLog += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                span = stopTime - startTime;
                TimerLog += "Total Time Start-Stop: " + span.ToString() + "\n";
            }
        }
    }
}
