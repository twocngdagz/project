using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class HTObservableObject : ObservableObject
    {
        private Hashtable data = new Hashtable();

        public virtual object getPropertyValue(string property_name)
        {
            return data[property_name];
        }

        public virtual void setPropertyValue(string property_name, object value)
        {
            data[property_name] = value;
            this.OnPropertyChanged(property_name);
        }
    }
}
