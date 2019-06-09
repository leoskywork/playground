using System;
using System.Collections.Concurrent;

namespace PlaygroundConsole
{
    class PlayMultiThreadingSet
    {
        public static void Run()
        {
            TestConcurrentDictionary();
        }

        private static void TestConcurrentDictionary()
        {
            var dictionary = new ConcurrentDictionary<string, string>();
            var key1 = "key1";
            var keyNull = default(string);

            if (dictionary.TryAdd(key1, "test"))
            {
                Console.WriteLine("key1 added");
            }

            dictionary.TryGetValue("", out _);

            if (dictionary.TryAdd(keyNull, "test null as key"))
            {
                Console.WriteLine("key null added");
            }
        }
    }
}
