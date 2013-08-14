using System;
using System.Threading;

namespace SimpleThread
{
    public class WriteThread
    {
        //private int _value;
        private SimpleQ<int> _queue; 

        public WriteThread(SimpleQ<int> queue)
        {
            _queue = queue;
        }

        //public WriteThread(SimpleQ<int> queue, int value)
        //{
        //    _queue = queue;
        //    _value = value;
        //}

        public void Commit(int value)
        {
            _queue.Enqueue(value);
        }

        public void StartWritting()
        {
            var rand = new Random();
            var commit = rand.Next();
            var loops = 10;
            //var endTime = DateTime.Now.AddSeconds(5);

            Console.WriteLine("Commencing write...");
            //while (endTime > DateTime.Now)
            while (loops != 0)
            {
                try
                {
                    Console.WriteLine("({0}) - Committing {1}", loops, commit);
                    Commit(commit);
                    Console.WriteLine("     Current queue: {0}", _queue.PrintAll());
                    commit = rand.Next();
                    loops--;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Caught write execption {0}", e.Message);
                }
            }
        }
    }

    public class ReadThread
    {
        private SimpleQ<int> _queue;

        public ReadThread(SimpleQ<int> queue)
        {
            _queue = queue;
        }

        public int Read()
        {
            return _queue.Dequeue();
        }

        public void StartReading()
        {
            //var endTime = DateTime.Now.AddSeconds(5);
            var loops = 10;

            Console.WriteLine("Commencing read...");
            //while (endTime > DateTime.Now)
            while (loops != 0)
            {
                try
                {
                    if (_queue.HasQueued())
                    {
                        var value = _queue.Dequeue();
                        Console.WriteLine("({0}) Dequeued {1}", loops, value);
                        loops--;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Caught read exception {0}", e.Message);
                }
            }
        }
    }

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
