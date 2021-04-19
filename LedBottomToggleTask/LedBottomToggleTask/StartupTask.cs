using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GrovePi;
using GrovePi.Sensors;


namespace LedBottomToggleTask
{
    public sealed class StartupTask : IBackgroundTask
    {
        ILed ledRed = DeviceFactory.Build.Led(Pin.DigitalPin5);
        IButtonSensor button = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin4);

        public static void Sleep(int NoOfMs)
        {
            Task.Delay(NoOfMs).Wait();
        }
        public void GetButton()
        {
            while (true)
            {
                string buttonState = button.CurrentState.ToString();
                if (buttonState.Equals("On"))
                {
                    Debug.WriteLine("Button is required");
                    ledRed.ChangeState(SensorStatus.On);
                    Sleep(200);
                    ledRed.ChangeState(SensorStatus.Off);
                    Sleep(200);
                }
            }
        }
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            var action = new Action(GetButton);
            Task.Run(action);
            while (true)
            {
                Debug.WriteLine("Checking button status");
                Sleep(2000);
            }
        }
    }
}
