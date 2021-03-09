using System;
using System.Device.Gpio;

namespace toggleSwitchLED
{
    class Program
    {
        private static GpioController controller = new GpioController();
        private static bool LightOn = false;
        private static int lastChangeTime = 0;
        static void Main(string[] args)
        {
            controller.OpenPin(17, PinMode.Output);
            controller.OpenPin(18, PinMode.InputPullUp);
            controller.RegisterCallbackForPinValueChangedEvent(18, PinEventTypes.Rising | PinEventTypes.Falling, OnPinChange);
            Console.ReadKey();
        }
        private static void OnPinChange(object sender, PinValueChangedEventArgs args)
        {
            if (DateTime.Now.Millisecond - lastChangeTime > 50)
            {
                if (args.ChangeType == PinEventTypes.Rising)
                {
                    ToggleLight();
                }
            }
            lastChangeTime = DateTime.Now.Millisecond;
        }
        static void ToggleLight()
        {
            LightOn = LightOn ? false : true;
            if (LightOn) { controller.Write(17, PinValue.High); }
            else { controller.Write(17, PinValue.Low); }
        }
    }
}
