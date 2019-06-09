using System;

namespace PlaygroundConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"--->{System.Threading.Thread.CurrentThread.ManagedThreadId}, Main() enter");

                PlayThreading.Run();
                //PlayMultiThreadingSet.Run();

                Console.WriteLine($"--->{System.Threading.Thread.CurrentThread.ManagedThreadId}, Main() exit");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--->{System.Threading.Thread.CurrentThread.ManagedThreadId}, Main() error: {ex}");
            }

            Console.ReadLine();
        }
    }
}
