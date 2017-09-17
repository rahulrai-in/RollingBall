using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptionTest
{
    using System.Net;
    using System.Threading;

    using Microsoft.PerceptionSimulation;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(
                async () =>
                    {
                        try
                        {
                            var sink = await RestSimulationStreamSink.Create(new Uri("http://169.254.172.219/"), new NetworkCredential("", ""), true, new CancellationToken());
                            var manager = PerceptionSimulationManager.CreatePerceptionSimulationManager(sink);
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.Home);
                            Thread.Sleep(2000);
                            // Simulate Bloom gesture again... this time, Shell should reappear
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.Home);
                            Thread.Sleep(2000);

                            // Simulate a Head rotation down around the X axis
                            // This should cause gaze to aim about the center of the screen
                            manager.Human.Head.Rotate(new Rotation3(0.04f, 0.0f, 0.0f));
                            Thread.Sleep(300);

                            // Simulate a finger press & release
                            // Should cause a tap on the center tile, thus launching it
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerPressed);
                            Thread.Sleep(300);
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerReleased);
                            Thread.Sleep(2000);

                            // Simulate a second finger press & release
                            // Should activate the app that was launched when the center tile was clicked
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerPressed);
                            Thread.Sleep(300);
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerReleased);
                            Thread.Sleep(5000);

                            // Simulate a Head rotation towards the upper right corner
                            manager.Human.Head.Rotate(new Rotation3(-0.14f, 0.17f, 0.0f));
                            Thread.Sleep(300);

                            // Simulate a third finger press & release
                            // Should press the Remove button on the app
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerPressed);
                            Thread.Sleep(300);
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.FingerReleased);
                            Thread.Sleep(2000);

                            // Simulate Bloom gesture again... bringing the Shell back once more
                            manager.Human.RightHand.PerformGesture(SimulatedGesture.Home);
                            Thread.Sleep(2000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    });

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}