using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Simple.Data.Core.Tests
{
    internal class DummyAdapter : Adapter
    {
        public DummyAdapter() : this(null)
        {
        }

        public DummyAdapter(string _)
        {

        }

        public DummyAdapter(string _, ILoggerFactory __)
        {
            
        }
        public override Task Execute(DataContext context)
        {
            return Task.FromResult<object>(null);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}