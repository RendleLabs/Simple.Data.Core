using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Simple.Data.Core.Commands
{
    public abstract class CommandBase
    {
        private readonly Wrangler _wrangler;

        protected CommandBase(Wrangler wrangler)
        {
            _wrangler = wrangler;
        }

        public virtual TaskAwaiter<dynamic> GetAwaiter()
        {
            return _wrangler.Execute(this).GetAwaiter();
        }
    }
}
