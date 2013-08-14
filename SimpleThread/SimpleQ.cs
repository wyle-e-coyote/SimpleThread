using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SimpleThread
{
    public class SimpleQ<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();

        public void Enqueue(T item)
        {
            Monitor.Enter(_queue);
            try
            {
                if (!Monitor.IsEntered(_queue))
                {
                    Monitor.Wait(_queue);
                }

                _queue.Enqueue(item);
            }
            finally 
            {
                Monitor.Exit(_queue);
            }
        }

        public bool TryEnqueue(T item)
        {
            if (Monitor.TryEnter(_queue))
            {
                try
                {
                    if (!Monitor.IsEntered(_queue))
                    {
                        Debug.WriteLine("Waiting for lock.");
                        Monitor.Wait(_queue);
                    }

                    _queue.Enqueue(item);

                    return true;
                }
                finally
                {
                    Monitor.Exit(_queue);
                }
            }
            return false;
        }

        public T Dequeue()
        {
            Monitor.Enter(_queue);
            try
            {
                if (!Monitor.IsEntered(_queue))
                {
                    Monitor.Wait(_queue);
                }
                return _queue.Dequeue();
            }
            finally
            {
                Monitor.Exit(_queue);
            }
        }

        public T Peek()
        {
            Monitor.Enter(_queue);
            try
            {
                if (!Monitor.IsEntered(_queue))
                {
                    Monitor.Wait(_queue);
                }
                
                return _queue.Count != 0 ? _queue.Peek() : default(T);
            }
            finally
            {
                Monitor.Exit(_queue);
            }
        }
        
        public bool HasQueued()
        {
            Monitor.Enter(_queue);
            try
            {
                if (!Monitor.IsEntered(_queue))
                {
                    Monitor.Wait(_queue);
                }

                return _queue.Count != 0;
            }
            finally
            {
                Monitor.Exit(_queue);
            }
        }

        public int Remove(T item)
        {
            Monitor.Enter(_queue);
            try
            {
                if (!Monitor.IsEntered(_queue))
                {
                    Monitor.Wait(_queue);
                }

                var removed = 0;
                var count = _queue.Count;
                while (count > 0)
                {
                    var elem = _queue.Dequeue();
                    if (elem.Equals(item))
                    {
                        removed++;
                    }
                    count--;
                }

                return removed;
            }
            finally 
            {
                Monitor.Exit(_queue);
            }
        }

        public string PrintAll()
        {
            Monitor.Enter(_queue);
            try
            {
                var output = new StringBuilder();
                foreach (var elem in _queue)
                {
                    output.AppendFormat("{0},", elem);
                }

                return output.ToString();
            }
            finally
            {
                Monitor.Exit(_queue);
            }
        }
    }
}
