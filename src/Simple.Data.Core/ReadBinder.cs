using System;
using System.Collections.Immutable;
using System.Dynamic;
using Simple.Data.Core.Commands;
using System.Linq;

namespace Simple.Data.Core
{
    public static class ReadBinder
    {
        private static readonly ImmutableDictionary<string, object> Empty = ImmutableDictionary<string, object>.Empty;

        public static ImmutableDictionary<string, object> ParseArgs(object[] args,InvokeBinder binder)
        {
            if (binder.CallInfo.ArgumentCount == 0) return Empty;
            if (binder.CallInfo.ArgumentCount == 1 && (binder.CallInfo.ArgumentNames.Count == 0 || string.IsNullOrWhiteSpace(binder.CallInfo.ArgumentNames[0])))
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