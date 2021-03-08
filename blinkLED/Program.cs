using System;
using System.Threading;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace blinkLED
{
    class Program
    {
        private const int LEDPin = 17;
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static CancellationToken ct = cts.Token;
        private static GpioController controller = new GpioController();
        static void Main(string[] args)
        {
            Task.Factory.StartNew(BlinkLight,ct);
            Console.ReadKey();
            cts.Cancel();
            controller.Dispose();
        }
        static void BlinkLight()
        {
            controller.OpenPin(LEDPin, PinMode.Output);
            PinValue pinState = PinValue.High;
            while(!ct.IsCancellationRequested){
                controller.Write(LEDPin, pinState);
                pinState = pinState.Equals(PinValue.High) ? PinValue.Low : PinValue.High;
                Thread.Sleep(1000);
            }
        }
    }
}
