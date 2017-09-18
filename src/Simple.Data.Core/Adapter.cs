using System;
using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public abstract class Adapter : IDisposable
    {
        public abstract Task Execute(DataContext context);

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}