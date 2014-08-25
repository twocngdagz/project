using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class Landlord : BaseLandlord
    {
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                if (value != null)
                {
                    string name = value.Trim();
                    int i = name.IndexOf(' ');
                    if (i != -1)
                    {
                        int i2 = name.LastIndexOf(' ');
                        this.Directory = name.Substring(0, i) + name.Substring(i2 + 1);
                    }
                    else
                        this.Directory = name;
                }
            }
        }
    }
}
