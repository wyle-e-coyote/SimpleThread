using System;
using System.Threading;

namespace SimpleThread
{
    class Program
    {
        static void Main()
        {
            //var instance = new SimpleClassThread();
            var instance = new ThreadWithState("Starting thread with value {0} in thread {1}", 42, ProgramCallback);
            Console.WriteLine("Main thread {0} starting InstanceMethod.", Thread.CurrentThread.GetHashCode());
            var thread0 = new Thread(instance.WorkerMethod);
            thread0.Start();
            
            //Console.WriteLine("Main thread {0} starting StaticMethod.", Thread.CurrentThread.GetHashCode());

            var now = DateTime.Now;

            var mutableString = "This is a simple string.";
            if (DateTime.Now > now)
                mutableString += " This is a continuation of the string.";

            Console.WriteLine(mutableString);

            var thread1 = new Thread(SimpleClassThread.StaticMethod);
            thread1.Start();

            thread0.Join();
            thread1.Join();

            Console.WriteLine("Main thread {0} completed.", Thread.CurrentThread.GetHashCode());

            Console.ReadLine();
        }

        private static readonly object Lock = new object();

        public static void ProgramCallback(int value)
        {
            lock (Lock)
            {
                Console.WriteLine("ProgramCallback was sent value {0} in thread {1}", value, Thread.CurrentThread.GetHashCode());
            }
        }
    }
}
