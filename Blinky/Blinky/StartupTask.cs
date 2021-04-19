using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;

using System.Diagnostics;
using System.Threading.Tasks;
using GrovePi;
using GrovePi.Sensors;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace Blinky
{
    public sealed class StartupTask : IBackgroundTask
    {
        //Use port DS for Red LED
        ILed ledRed = DeviceFactory.Build.Led(Pin.DigitalPin5);

        //This is created to let the program wait for the specified number
        private void Sleep(int NoOfMs)
        {
            Task.Delay(NoOfMs).Wait();
        }
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            while (true)
            {
                Sleep(300);

                if (ledRed.CurrentState == SensorStatus.Off)
                {
                    ledRed.ChangeState(SensorStatus.On);
                    Debug.WriteLine("Turning ON LED");
                }
                else
                {
                    ledRed.ChangeState(SensorStatus.Off);
                    Debug.WriteLine("Turning OFF LED");
                }
            }
        }
    }
}
