using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Data.Core
{
    public class SimpleRecord : DynamicObject
    {
        private readonly IReadOnlyDictionary<string, object> _data;
        public SimpleRecord(IReadOnlyDictionary<string, object> data)
        {
            _data = data;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _data.TryGetValue(binder.Name, out result) || base.TryGetMember(binder, out result);
        }
    }
}