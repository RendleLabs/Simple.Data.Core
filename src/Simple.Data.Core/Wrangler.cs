using System;
using System.Dynamic;
using System.Threading.Tasks;
using Simple.Data.Core.Commands;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core
{
    public class Wrangler : IDisposable
    {
        private readonly Adapter _adapter;

        public Wrangler(Adapter adapter)
        {
            _adapter = adapter;
        }

        public bool Invoke(Thing thing, InvokeBinder binder, object[] args, out object result)
        {
            switch (thing.Name[0])
            {
                case 'I':
                    if (thing.Name.Equals("Insert", StringComparison.OrdinalIgnoreCase))
                    {
                        var table = thing.Parent.AsTable();
                        result = new InsertCommand(this, table, ReadBinder.ParseArgs(args, binder));
                        return true;
                    }
                    break;
                case 'F':
                    if (thing.Name.Equals("Find", StringComparison.OrdinalIgnoreCase))
                    {
                        var table = thing.Parent.AsTable();
                        result = new FindCommand(this, table, ExpressionFromBinder.Parse(table, args, binder));
                        return true;
                    }
                    if (thing.Name.StartsWith("FindBy"))
                    {
                        var table = thing.Parent.AsTable();
                        var column = new Column(thing.Name.Substring(6), table);
                        result = new FindByCommand(this, table, column, args[0]);
                        return true;
                    }
                    break;
                case 'U':
                    if (thing.Name.StartsWith("UpdateBy"))
                    {
                        var table = thing.Parent.AsTable();
                        var column = new Column(thing.Name.Substring(8), table);
                        var criteria = SimpleExpression.Equal(column, args[0]);
                        result = new UpdateCommand(this, table, criteria, ReadBinder.ParseArgs(args, binder));
                        return true;
                    }
                    break;
                case 'G':
                    if (thing.Name.StartsWith("GetBy"))
                    {
                        var table = thing.Parent.AsTable();
                        var column = new Column(thing.Name.Substring(5), table);
                        result = new GetByCommand(this, table, column, args[0]);
                        return true;
                    }
                    break;
            }
            result = null;
            return false;
        }

        public Task<dynamic> Execute(CommandBase command, Func<object, object> finish)
        {
            return Execute<object>(command, finish);
        }

        public async Task<T> Execute<T>(CommandBase command, Func<object, T> finish)
        {
            var context = new DataContext();
            context.Request.Command = command;
            await _adapter.Execute(context).ConfigureAwait(false);
            return finish(context.Response.Result);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}