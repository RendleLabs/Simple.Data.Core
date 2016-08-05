using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Simple.Data.Core.Expressions;

namespace Simple.Data.Core
{
    public class Thing : DynamicObject
    {
        private readonly Wrangler _wrangler;
        public string Name { get; }
        public Thing Parent { get; }

        public Thing(string name, Wrangler wrangler, Thing parent = null)
        {
            if (wrangler == null) throw new ArgumentNullException(nameof(wrangler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            _wrangler = wrangler;
            Name = name;
            Parent = parent;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new Thing(binder.Name, _wrangler, this);
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            return _wrangler.Invoke(this, binder, args, out result);
        }

        public Container AsContainer()
        {
            return new Container(Name);
        }

        public Table AsTable()
        {
            return Parent == null ? new Table(Name) : new Table(Name, Parent.AsContainer());
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            throw new NotImplementedException();
        }
    }
}
