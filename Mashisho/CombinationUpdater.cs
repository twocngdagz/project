using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class CombinationUpdater<T> : IUpdater<T>
    {
        private List<IUpdater<T>> updaters = new List<IUpdater<T>>();
        public void AddUpdater(IUpdater<T> updater)
        {
            updaters.Add(updater);
        }

        public T Add(T t)
        {
            foreach (IUpdater<T> updater in updaters)
            {
                t = updater.Add(t);
            }
            return t;
        }

        public void Delete(T t)
        {
            foreach (IUpdater<T> updater in updaters)
            {
                updater.Delete(t);
            }
        }

        public void Update(T from, T to)
        {
            foreach (IUpdater<T> updater in updaters)
            {
                updater.Update(from, to);
            }
        }
    }
}
