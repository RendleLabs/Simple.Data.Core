using System.Dynamic;
using System.Threading.Tasks;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core
{
    public class Wrangler
    {
        private readonly Adapter _adapter;

        public Wrangler(Adapter adapter)
        {
            _adapter = adapter;
        }

        public bool Invoke(Thing thing, InvokeBinder binder, object[] args, out object result)
        {
            if (thing.Name.StartsWith("GetBy"))
            {
                var table = thing.AsTable();
                var column = new Column(thing.Name.Substring(5), table);
                result = new GetByCommand(this, thing.AsTable(), column, args[0]);
                return true;
            }
            result = null;
            return false;
        }

        public async Task<dynamic> Execute(CommandBase command)
        {
            var context = new DataContext();
            context.Request.Command = command;
            await _adapter.Execute(context).ConfigureAwait(false);
            return context.Response.Result;
        }
    }
}