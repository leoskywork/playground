using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaygroundConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlayThreading.Run();
            PlayMultiThreadingSet.Run();

            Console.WriteLine($"--->{System.Threading.Thread.CurrentThread.ManagedThreadId}, main thread end");
            Console.ReadLine();
        }
    }
}
