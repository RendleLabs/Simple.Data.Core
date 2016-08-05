using System.Threading.Tasks;
using Simple.Data.Core.Commands;

namespace Simple.Data.Core
{
    public class Execution
    {
        private readonly Adapter _adapter;

        public Execution(Adapter adapter)
        {
            _adapter = adapter;
        }

        public Task Execute(ICommand command)
        {
            var context = new DataContext();
            context.Request.Command = command;
            return _adapter.Execute(context);
        }
    }
}