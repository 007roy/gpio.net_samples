using System;
using System.Threading;
using System.Threading.Tasks;
using System.Device.Pwm;

namespace dimingLED
{
class Program
    {
        private static PwmChannel pwm = PwmChannel.Create(0,0,500,0);
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static CancellationToken ct = cts.Token;
        static void Main(string[] args)
        {
            Task.Factory.StartNew(Breath,ct);
            Console.ReadKey();
            cts.Cancel();
            pwm.Stop();
        }

        private static void Breath(){
            pwm.Start();
            do{
                for(int i = 1; i <=100; i++)
                {
                    double dc = (double)i/100;
                    pwm.DutyCycle = dc;
                    ct.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                }
                Thread.Sleep(1000);
                for(int i = 100; i >= 1; i--)
                {
                    double dc = (double)i/100;
                    pwm.DutyCycle = dc;
                    ct.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                }
                Thread.Sleep(1000);
            }while(true);
        }
    }
}
