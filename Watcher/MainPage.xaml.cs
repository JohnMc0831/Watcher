using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public MainPage()
        {
            this.InitializeComponent();
            Task.Run(async () =>
            {
                ISenseHat hat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);
                var sensors = new ReadAllSensors(hat);
                ISenseHatSensors sensorReadings = sensors.Run();
                string temp = sensorReadings.Temperature?.ToString() ?? "N/A";
                Debug.WriteLine($"TEMP: {temp} Celsius");
                var t = new WatcherTemperature(Convert.ToDouble(temp));
                Debug.WriteLine($"TEMP: {t.ToFahrenheit()} Fahrenheit");
                Debug.WriteLine($"HUMIDITY: {sensorReadings.Humidity?.ToString()}");
                Debug.WriteLine($"BAROMETRIC PRESSURE: {sensorReadings.Pressure?.ToString()}");
                Debug.WriteLine($"MAGNETOMETER: {sensorReadings.MagneticField?.ToString()}");
                Debug.WriteLine($"");
            });
        }
    }
}
