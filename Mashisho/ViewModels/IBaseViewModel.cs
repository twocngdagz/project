using System;
using System.ComponentModel;

namespace Mashisho
{
    public abstract class IBaseViewModel : ObservableObject, IUpdater<BaseObject>
    {
        public ICollectionView AllObjects { get; set; }
        public HTObservableObject MyObjectFilter { get; set; }
        public BaseObject MyPartialObject { get; set; }

        public abstract BaseObject Add(BaseObject ofrom);
        public abstract void Delete(BaseObject o);
        public abstract void Update(BaseObject ofrom, BaseObject oto);
    }
}
