using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    interface IPartialObject
    {
        void Add();
        void Update();
        void Delete();

        void Clear();
    }
}
