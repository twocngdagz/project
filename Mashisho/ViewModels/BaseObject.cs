using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseObject : HTObservableObject
    {
        public virtual void SetFrom(BaseObject other, bool full = true) { }
    }
}
