using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Data.Core.Commands
{
    public struct ArgumentEnumerable : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly object[] _args;
        private readonly InvokeBinder _binder;

        public ArgumentEnumerable(object[] args, InvokeBinder binder)
        {
            _args = args;
            _binder = binder;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return new ArgumentEnumerator(_args, _binder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct ArgumentEnumerator : IEnumerator<KeyValuePair<string, object>>
        {
            private int _index;
            private readonly object[] _args;
            private readonly InvokeBinder _binder;

            public ArgumentEnumerator(object[] args, InvokeBinder binder)
            {
                _index = -1;
                _args = args;
                _binder = binder;
            }

            public bool MoveNext()
            {
                return ++_index < _args.Length;
            }

            public void Reset()
            {
                _index = -1;
            }

            public KeyValuePair<string, object> Current
                => new KeyValuePair<string, object>(_binder.CallInfo.ArgumentNames[_index], _args[_index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}