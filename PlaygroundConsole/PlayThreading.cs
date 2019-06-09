using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    class PlayThreading
    {
        public static void Run()
        {
            //TestCancellationToken_Callback();
            //TestTask_StartAfterCancel();
            TestTask_StartBeforeCancel();
        }

        //throw System.InvalidOperationException
        private static void TestTask_StartAfterCancel()
        {
            var source = new CancellationTokenSource();
            var task = new Task(() =>
            {
                Console.WriteLine("task action running, thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);

                Console.WriteLine("task action done, thread: " + Thread.CurrentThread.ManagedThreadId);
            }, source.Token);

            source.Cancel();
              
        }

        private static void TestTask_StartBeforeCancel()
        {
            var source = new CancellationTokenSource();
            var task = new Task(() =>
            {
                Console.WriteLine("task action 2 running, thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);

                Console.WriteLine("task action 2 done, thread: " + Thread.CurrentThread.ManagedThreadId);
            }, source.Token);

            
            task.Start();
            //Thread.Sleep(20);
            source.Cancel();

            var source3 = new CancellationTokenSource();
            var task3 = new Task(() =>
            {
                Console.WriteLine("task action 3 running, thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);

                Console.WriteLine("task action 3 done, thread: " + Thread.CurrentThread.ManagedThreadId);
            }, source3.Token);


            //task3.Start();
            
        }

        private static void TestCancellationToken_Callback()
        {
            var source = new CancellationTokenSource();
            source.Token.Register(() => Console.WriteLine("canceled callback 1"));
            source.Token.Register(() => Console.WriteLine("canceled callback 2"));
            var reg3 = source.Token.Register(() => Console.WriteLine("canceled callback 3"));
            source.Token.Register(() => Console.WriteLine("canceled callback 4"));

            reg3.Dispose();
            source.Cancel();

            //var linkedSrc = CancellationTokenSource.CreateLinkedTokenSource(CancellationToken.None, CancellationToken.None);


        }
    }
}
