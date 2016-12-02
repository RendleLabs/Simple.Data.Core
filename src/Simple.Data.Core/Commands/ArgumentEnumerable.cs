using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Data.Core.Commands
{
    public struct ArgumentEnumerable : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly object[] _args;
        private readonly InvokeBinder _binder;
        private readonly int _unnamedCount;

        public ArgumentEnumerable(object[] args, InvokeBinder binder)
        {
            _args = args;
            _binder = binder;
            _unnamedCount = args.Length - binder.CallInfo.ArgumentNames.Count;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return new ArgumentEnumerator(_args, _binder, _unnamedCount);
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
            private readonly int _unnamedCount;

            public ArgumentEnumerator(object[] args, InvokeBinder binder, int unnamedCount)
            {
                _index = unnamedCount - 1;
                _args = args;
                _binder = binder;
                _unnamedCount = unnamedCount;
            }

            public bool MoveNext()
            {
                return ++_index < _args.Length;
            }

            public void Reset()
            {
                _index = _unnamedCount - 1;
            }

            public KeyValuePair<string, object> Current
                => new KeyValuePair<string, object>(_binder.CallInfo.ArgumentNames[_index - _unnamedCount], _args[_index]);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}