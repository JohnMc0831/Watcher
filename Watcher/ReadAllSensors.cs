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

namespace Watcher
{
	public class ReadAllSensors
	{
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

	            //Populate the ViewModel object.
	            //WatcherViewModel model = new WatcherViewModel();
	            //model.Humidity.SetHumidity(SenseHat.Sensors.Humidity);
	            //model.Temperature.SetTemperature(SenseHat.Sensors.Temperature);
	            //model.BarometricPressure.SetPressure(SenseHat.Sensors.Pressure);
	            //model.Pose.X = SenseHat.Sensors.Pose?.X;
	            //model.Pose.Y = SenseHat.Sensors.Pose?.Y;
	            //model.Pose.Z = SenseHat.Sensors.Pose?.Z;
	            //model.Magnetometer.X = SenseHat.Sensors.MagneticField?.X;
	            //model.Magnetometer.Y = SenseHat.Sensors.MagneticField?.Y;
	            //model.Magnetometer.Z = SenseHat.Sensors.MagneticField?.Z;
	            //model.Accelerometer.X = SenseHat.Sensors.Acceleration?.X;
	            //model.Accelerometer.Y = SenseHat.Sensors.Acceleration?.Y;
	            //model.Accelerometer.Z = SenseHat.Sensors.Acceleration?.Z;
	            //model.Gyroscope.X = SenseHat.Sensors.Gyro?.X;
	            //model.Gyroscope.Y = SenseHat.Sensors.Gyro?.Y;
	            //model.Gyroscope.Z = SenseHat.Sensors.Gyro?.Z;
	            // Build up the string
	            //stringBuilder.Clear();
	            //stringBuilder.AppendLine(
	            //    $"Gyro: {SenseHat.Sensors.Gyro?.ToString(false) ?? "N/A"}"); // From the ImuSensor.
	            //stringBuilder.AppendLine(
	            //    $"Accel: {SenseHat.Sensors.Acceleration?.ToString(false) ?? "N/A"}"); // From the ImuSensor.
	            //stringBuilder.AppendLine(
	            //    $"Mag: {SenseHat.Sensors.MagneticField?.ToString(false) ?? "N/A"}"); // From the ImuSensor.
	            //stringBuilder.AppendLine(
	            //    $"Pose: {SenseHat.Sensors.Pose?.ToString(false) ?? "N/A"}"); // From the ImuSensor.
	            //stringBuilder.AppendLine(
	            //    $"Press: {SenseHat.Sensors.Pressure?.ToString() ?? "N/A"}"); // From the PressureSensor.
	            //stringBuilder.AppendLine(
	            //    $"Temp: {SenseHat.Sensors.Temperature?.ToString() ?? "N/A"}"); // From the HumiditySensor.
	            //stringBuilder.AppendLine(
	            //    $"Hum: {SenseHat.Sensors.Humidity?.ToString() ?? "N/A"}"); // From the HumiditySensor.

	            ////if ((SetScreenText != null) && nextMainPageUpdate <= DateTime.Now)
	            ////{
	            ////	SetScreenText(stringBuilder.ToString());
	            ////	nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);
	            ////}

	            //Debug.WriteLine(stringBuilder.ToString());
                //return stringBuilder.ToString();
	        }
	    }
	}
}