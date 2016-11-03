using System.Collections.Immutable;
using System.Dynamic;

namespace Simple.Data.Core.Commands
{
    public class ReadBinder
    {
        private static readonly ImmutableDictionary<string, object> Empty = ImmutableDictionary<string, object>.Empty;

        public ImmutableDictionary<string, object> ParseParameters(object[] args,InvokeBinder binder)
        {
            if (binder.CallInfo.ArgumentCount == 0) return Empty;
            if (binder.CallInfo.ArgumentCount == 1 && string.IsNullOrWhiteSpace(binder.CallInfo.ArgumentNames[0]))
            {
                return ObjectToDictionary(args[0]);
            }
            return ArgsToDictionary(args, binder);
        }

        private static ImmutableDictionary<string, object> ObjectToDictionary(object obj)
        {
            return Empty.AddRange(new ObjectEnumerable(obj));
        }

        private static ImmutableDictionary<string, object> ArgsToDictionary(object[] args, InvokeBinder binder)
        {
            return Empty.AddRange(new ArgumentEnumerable(args, binder));
        }
    }

}