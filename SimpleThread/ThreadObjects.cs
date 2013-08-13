using System;
using System.Threading;

namespace SimpleThread
{
    public class SimpleClassThread
    {
        public void InstanceMethod()
        {
            Console.WriteLine("InstanceMethod has been called in a different thread: {0}", Thread.CurrentThread.GetHashCode());

            Thread.Sleep(4000);

            Console.WriteLine("InstanceMethod is exiting from thread: {0}", Thread.CurrentThread.GetHashCode());
        }

        public static void StaticMethod()
        {
            Console.WriteLine("StaticMethod has been called in a differeent thread: {0}", Thread.CurrentThread.GetHashCode());

            Thread.Sleep(3000);

            Console.WriteLine("StaticMethod is exiting from thread {0}", Thread.CurrentThread.GetHashCode());
        }
    }

    public class ThreadWithState
    {
        private string _text;
        private int _value;
        private ExampleCallback _callback;

        public ThreadWithState(string text, int value, ExampleCallback callback)
        {
            _text = text;
            _value = value;
            _callback = callback;
        }

        public void WorkerMethod()
        {
            Console.WriteLine(_text, _value, Thread.CurrentThread.GetHashCode());
            Thread.Sleep(3000);
            if (_callback != null)
            {
                _callback(_value);
            }
        }
    }

    public delegate void ExampleCallback(int lineCount);
}
