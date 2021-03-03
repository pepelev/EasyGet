using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyGet
{
    internal sealed class Cached<T>
    {
        private volatile Func<Task<T>> factory;
        private volatile Lazy<Task<T>> task;

        public Cached(Func<Task<T>> factory)
        {
            this.factory = factory;
            task = new Lazy<Task<T>>(factory, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public async Task<T> ValueAsync()
        {
            var currentTask = task;
            try
            {
                var result = await currentTask.Value;
                factory = null;
                return result;
            }
            catch
            {
                var currentFactory = factory;
                if (currentFactory != null)
                {
                    var newTask = new Lazy<Task<T>>(currentFactory, LazyThreadSafetyMode.ExecutionAndPublication);
                    Interlocked.CompareExchange(ref task, newTask, currentTask);
                }

                throw;
            }
        }
    }
}