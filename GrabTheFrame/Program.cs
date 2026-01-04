using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabTheFrame
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine(" Frame Processing Comparison Demo ");
            Console.WriteLine("=======================================\n");

            Console.WriteLine("Choose version:");
            Console.WriteLine("1 = Intern Version (Buggy & Unsafe)");
            Console.WriteLine("2 = Correct Version (Safe & Deterministic)");
            Console.Write("\nYour choice: ");

            var key = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");

            var reporter = new FakeValueReporter();

            if (key == '1')
            {
                Console.WriteLine(">>> RUNNING INTERN VERSION");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Expected issues:");
                Console.WriteLine("- Shared buffer reused");
                Console.WriteLine("- Data corruption possible");
                Console.WriteLine("- Queue not thread-safe");
                Console.WriteLine("- Dispose pattern misleading");
                Console.WriteLine("- Timer not synchronized with camera\n");

                var grabber = new FrameGrabber();
                var intern = new InternFrameProcessor(grabber, reporter);
                intern.Start();

                var camera = new FakeCamera(grabber, 4, 4);
                camera.Start(5);
            }
            else
            {
                Console.WriteLine(">>> RUNNING CORRECT VERSION");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Design characteristics:");
                Console.WriteLine("- Deep copy inside callback");
                Console.WriteLine("- No shared mutable state");
                Console.WriteLine("- Deterministic processing");
                Console.WriteLine("- Native interop safe");
                Console.WriteLine("- Real-time friendly\n");

                var processor = new CorrectFrameProcessor(reporter);
                var camera = new FakeCamera(processor, 4, 4);
                camera.Start(5);
            }

            Console.WriteLine("\n=======================================");
            Console.WriteLine("Execution finished.");
            Console.WriteLine("Compare the reported averages above.");
            Console.WriteLine("Press ENTER to exit...");
            Console.WriteLine("=======================================\n");

            Console.ReadLine();
        }
    }


}
