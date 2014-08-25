using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public interface IUpdater<T>
    {
        T Add(T t);
        void Delete(T t);
        void Update(T from, T to);
    }
}
