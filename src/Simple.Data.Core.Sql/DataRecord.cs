using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Simple.Data.Core.Sql
{
    internal class DataRecord : IReadOnlyDictionary<string, object>
    {
        private readonly IReadOnlyDictionary<string, int> _keyIndexes;
        private readonly object[] _values;

        private DataRecord(IReadOnlyDictionary<string, int> keyIndexes, object[] values)
        {
            _keyIndexes = keyIndexes;
            _values = values;
        }

        public static DataRecord FromDataReader(IReadOnlyDictionary<string, int> keyIndexes, DbDataReader reader)
        {
            var values = new object[keyIndexes.Count];
            reader.GetValues(values);
            return new DataRecord(keyIndexes, values);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            var values = _values;
            return
                _keyIndexes.Select(kvp => new KeyValuePair<string, object>(kvp.Key, values[kvp.Value])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _keyIndexes.Count;

        public bool ContainsKey(string key) => _keyIndexes.ContainsKey(key);

        public bool TryGetValue(string key, out object value)
        {
            int index;
            if (_keyIndexes.TryGetValue(key, out index))
            {
                value = _values[index];
                return true;
            }
            value = null;
            return false;
        }

        public object this[string key]
        {
            get
            {
                object value;
                if (TryGetValue(key, out value))
                {
                    return value;
                }
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<string> Keys => _keyIndexes.Keys;
        public IEnumerable<object> Values => _values.AsEnumerable();

        public static IReadOnlyDictionary<string, int> CreateIndex(DbDataReader reader)
        {
            var keyIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                keyIndexes.Add(reader.GetName(i), i);
            }
            return keyIndexes;
        }
    }
}