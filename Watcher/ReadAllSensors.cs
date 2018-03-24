////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2017, Mattias Larsson
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of 
//  this software and associated documentation files (the "Software"), to deal in 
//  the Software without restriction, including without limitation the rights to use, 
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
//  Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Text;
using Emmellsoft.IoT.Rpi.SenseHat;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Watcher
{
	public class ReadAllSensors
	{
	    DispatcherTimer dispatcherTimer;
	    DateTimeOffset startTime;
	    DateTimeOffset lastTime;
	    DateTimeOffset stopTime;
	    int timesTicked = 1;
	    int timesToTick = 10;
	    private string TimerLog = string.Empty;
	    private ISenseHatSensors sensors;

        public ReadAllSensors(ISenseHat senseHat)
	    {
	    }

        public ISenseHat SenseHat = SenseHatFactory.GetSenseHat().Result;

	    public ISenseHatSensors Run()
	    {
	        TimeSpan mainPageUpdateRate = TimeSpan.FromSeconds(0.5);
	        DateTime nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);

	        while (true)
	        {
	            Task.Delay(TimeSpan.FromSeconds(50));
                SenseHat.Sensors.ImuSensor.Update(); // Try get a new read-out for the Gyro, Acceleration, MagneticField and Pose.
	            SenseHat.Sensors.PressureSensor.Update(); // Try get a new read-out for the Pressure.
	            SenseHat.Sensors.HumiditySensor.Update(); // Try get a new read-out for the Temperature and Humidity.
	            return SenseHat.Sensors;
	        }
	    }
    }
}