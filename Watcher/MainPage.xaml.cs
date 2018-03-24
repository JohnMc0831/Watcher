using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Emmellsoft.IoT.Rpi.SenseHat;


namespace Watcher
{
    public sealed partial class MainPage : Page
    {
        public WatcherViewModel Model;
        public ISenseHat hat;
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 3;
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
                //DispatcherTimerSetup();
                UpdateSensors();
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
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
                });
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
                Debug.WriteLine($"% RELATIVE HUMIDITY: {sensorReadings.Humidity?.ToString("F")}");
                results += $"\n% RELATIVE HUMIDITY: {sensorReadings.Humidity?.ToString("F")}";
                Debug.WriteLine($"BAROMETRIC PRESSURE (MB): {sensorReadings.Pressure?.ToString("F")}");
                results += $"\nBAROMETRIC PRESSURE (MB): {sensorReadings.Pressure?.ToString("F")}";
                Debug.WriteLine($"MAGNETOMETER: {sensorReadings.MagneticField?.ToString()}");
                results += $"\nMAGNETOMETER: {sensorReadings.MagneticField?.ToString()}";

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    //update the UI
                    this.TempText.Text = results;
                    this.TempText.UpdateLayout();
                });
            });
        }

       void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            TimerLog += timesTicked + "\t time since last tick: " + span.ToString() + "\n";
            Debug.WriteLine(TimerLog);
            timesTicked++;
            if (timesTicked > timesToTick)
            {
                stopTime = time;
                TimerLog += "Calling dispatcherTimer.Stop()\n";
                dispatcherTimer.Stop();
                UpdateSensors();
                TimerLog += "dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled + "\n";
                span = stopTime - startTime;
                TimerLog += "Total Time Start-Stop: " + span.ToString() + "\n";
                Debug.WriteLine(TimerLog);
                dispatcherTimer.Start();
            }
        }
    }
}
