using System;
using System.Collections;
using System.Collections.Generic;

namespace Simple.Data.Core.Commands
{
    internal struct NullEnumerator<T> : IEnumerator<T>
    {
        public T Current
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        {
        }
    }
}