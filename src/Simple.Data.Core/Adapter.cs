using System;
using System.Threading.Tasks;

namespace Simple.Data.Core
{
    public abstract class Adapter : IDisposable
    {
        public abstract Task Execute(DataContext context);
        public abstract void Dispose();
    }
}