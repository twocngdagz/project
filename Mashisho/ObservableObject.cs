using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetAndNotify<T>(ref T field, T value, string property_name)
        {
            bool are_equal = (typeof(T).IsByRef && object.ReferenceEquals(field, value))
                          || ((!typeof(T).IsByRef) && object.Equals(field, value));


            if (!are_equal)
            {
                field = value;
                this.OnPropertyChanged(property_name);
            }
        }

        protected virtual void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }
    }
}
