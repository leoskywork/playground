using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    class PlayThreading
    {
        public static void Run()
        {
            //TestTask_StartAfterCancel();
            // TestTask_StartBeforeCancel();

            //TestCancellationToken_MultiCallbacksInvokeOrder();
            //TestCancellationToken_CallbackInvoke_CompareTo_TaskActionInvoke();
            TestCancellationToken_HardCancel();
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
            task.Start();
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

        private static void TestCancellationToken_MultiCallbacksInvokeOrder()
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

        private static void TestCancellationToken_CallbackInvoke_CompareTo_TaskActionInvoke()
        {
            var source = new CancellationTokenSource();
            source.Token.Register(() => Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", cxl token callback - default sync context"));
            source.Token.Register(() => Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", cxl token callback - true sync context"), true);
            source.Token.Register(() => Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", cxl token callback - false sync context"), false);

            Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", task action enter");

                Thread.Sleep(1000);

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", task action leave");
            });

            source.Cancel();
        }

        private static void TestCancellationToken_HardCancel()
        {
            var source = new CancellationTokenSource();
            Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", task enter");

                HardCancel_LongRun(source.Token);
                //Thread.Sleep(3000);

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", task leave");
            });

            Thread.Sleep(2 * 1000);
            source.Cancel();
        }

        private static void HardCancel_LongRun(CancellationToken token)
        {
            Thread longRunThread = null;

            using (
            token.Register(() =>
         {
             Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", cxl token callback");

             //not working
             //Thread.CurrentThread.Abort();

             Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", cxl token callback - longRunThread is null: " +(longRunThread==null));
             if (longRunThread != null)
             {
                 longRunThread.Abort();
             }

         }, useSynchronizationContext: true)//;
            )
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", long run enter");

                longRunThread = Thread.CurrentThread;
                Thread.Sleep(5 * 1000);
                longRunThread = null;

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ", long run leave");
            };
            //false - abort main thread
            //true - abort main thread
            //default - abort main thread

            //without using statement
            //default(no second argument) - abort main thread
            //true - abort main thread
            //false - abort main thread


        }

        private static void TestTaskCompletionSource()
        {
            var completionSource = new TaskCompletionSource<string>();

            //completionSource.
        }
    }
}
