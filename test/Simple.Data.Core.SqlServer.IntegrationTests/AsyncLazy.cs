using System;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Data.Core.SqlServer.IntegrationTests
{
    public class AsyncLazy<T>
    {
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
        private readonly Func<Task<T>> _creator;
        private T _value;

        public AsyncLazy(Func<Task<T>> creator)
        {
            _creator = creator;
        }

        public async Task<T> Value()
        {
            if (_value == null)
            {
                await _lock.WaitAsync();
                try
                {
                    _value = await _creator();
                }
                finally
                {
                    _lock.Release();
                }
            }
            return _value;
        }
    }
}