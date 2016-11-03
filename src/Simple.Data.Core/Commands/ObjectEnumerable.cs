using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simple.Data.Core.Commands
{
    internal struct ObjectEnumerable : IEnumerable<KeyValuePair<string, object>>
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyInfos =
            new ConcurrentDictionary<Type, PropertyInfo[]>();

        private readonly object _source;

        public ObjectEnumerable(object source)
        {
            _source = source;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if (_source == null) return new NullEnumerator<KeyValuePair<string, object>>();
            return new ObjectEnumerator(_source,
                PropertyInfos.GetOrAdd(_source.GetType(), t => t.GetTypeInfo().DeclaredProperties.ToArray()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct ObjectEnumerator : IEnumerator<KeyValuePair<string, object>>
        {
            private readonly object _source;
            private readonly PropertyInfo[] _propertyInfos;
            private int _index;

            public ObjectEnumerator(object source, PropertyInfo[] propertyInfos)
            {
                _source = source;
                _propertyInfos = propertyInfos;
                _index = -1;
            }

            public bool MoveNext()
            {
                return ++_index < _propertyInfos.Length;
            }

            public void Reset()
            {
                _index = -1;
            }

            public KeyValuePair<string, object> Current
                =>
                new KeyValuePair<string, object>(_propertyInfos[_index].Name, _propertyInfos[_index].GetValue(_source));

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}