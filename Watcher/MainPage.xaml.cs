using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Watcher;
using Emmellsoft.IoT.Rpi;
using Emmellsoft.IoT.Rpi.SenseHat;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Watcher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public WatcherViewModel Model;
        public ISenseHat hat;
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 10;
        private string TimerLog = string.Empty;
        private ISenseHatSensors sensors;
        private string results = string.Empty;

        public MainPage()
        {
            this.InitializeComponent();
            Task.Run(async () =>
            {
                hat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);
                //UpdateSensors();
                DispatcherTimerSetup();
                UpdateSensors();
                //var sensors = new ReadAllSensors(hat);
                //ISenseHatSensors sensorReadings = sensors.Run();
                //string temp = sensorReadings.Temperature?.ToString() ?? "N/A";
                //Debug.WriteLine($"TEMP: {temp} Celsius");
                //var t = new WatcherTemperature(Convert.ToDouble(temp));
                //results = $"TEMP: {t.Celsius?.ToString("F")} Celsius";
                //Debug.WriteLine($"TEMP: {Math.Round(t.ToFahrenheit(), 2)} Fahrenheit");
                //results += $"\nTEMP: {t.ToFahrenheit()} Fahrenheit";
                //Debug.WriteLine($"HUMIDITY: {sensorReadings.Humidity?.ToString("F")}");
                //results += $"\nHUMIDITY: {sensorReadings.Humidity?.ToString("F")}";
                //Debug.WriteLine($"BAROMETRIC PRESSURE: {sensorReadings.Pressure?.ToString("F")}");
                //results += $"\nBAROMETRIC PRESSURE: {sensorReadings.Pressure?.ToString("F")}";
                //Debug.WriteLine($"MAGNETOMETER: {sensorReadings.MagneticField?.ToString()}");
                //results += $"\nMAGNETOMETER: {sensorReadings.MagneticField?.ToString()}";

                ////update the UI
                //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                //    //Temperature
                //    this.TempText.Text = results;
                //    this.TempText.UpdateLayout();
                //});

            });           
        }

        public void UpdateSensors()
        {
            Task.Run(async () =>
            {
                var sensors = new ReadAllSensors(hat);
                ISenseHatSensors sensorReadings = sensors.Run();
                string temp = sensorReadings.Temperature?.ToString() ?? "N/A";
                Debug.WriteLine($"TEMP: {temp} Celsius");
                var t = new WatcherTemperature(Convert.ToDouble(temp));
                results = $"TEMP: {t.Celsius?.ToString("F")} Celsius";
                Debug.WriteLine($"TEMP: {Math.Round(t.ToFahrenheit(), 2)} Fahrenheit");
                results += $"\nTEMP: {t.ToFahrenheit()} Fahrenheit";
                Debug.WriteLine($"HUMIDITY: {sensorReadings.Humidity?.ToString("F")}");
                results += $"\nHUMIDITY: {sensorReadings.Humidity?.ToString("F")}";
                Debug.WriteLine($"BAROMETRIC PRESSURE: {sensorReadings.Pressure?.ToString("F")}");
                results += $"\nBAROMETRIC PRESSURE: {sensorReadings.Pressure?.ToString("F")}";
                Debug.WriteLine($"MAGNETOMETER: {sensorReadings.MagneticField?.ToString()}");
                results += $"\nMAGNETOMETER: {sensorReadings.MagneticField?.ToString()}";

                //update the UI
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    //Temperature
                    this.TempText.Text = results;
                    this.TempText.UpdateLayout();
                });

            });
        }

        void DispatcherTimerSetup()
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

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
                UpdateSensors();
                //IsEnabled should now be false after calling stop
                TimerLog += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                span = stopTime - startTime;
                TimerLog += "Total Time Start-Stop: " + span.ToString() + "\n";
                Debug.WriteLine(TimerLog);
            }
        }
    }
}
