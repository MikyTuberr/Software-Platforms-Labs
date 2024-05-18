using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PT_10
{
    internal class SortableSearchableBindingList<T> : BindingList<T>
    {
        private bool isSorted;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (prop == null)
            {
                throw new ArgumentNullException(nameof(prop));
            }

            sortProperty = prop;
            sortDirection = direction;

            List<T> items = this.Items as List<T>;
            if (items != null)
            {
                IComparer<T> comparer = Comparer<T>.Default;
                items.Sort(comparer);

                if (direction == ListSortDirection.Descending)
                {
                    items.Reverse();
                }
            }
            isSorted = true;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            for (int i = 0; i < Count; i++)
            {
                T item = Items[i];
                if (prop.GetValue(item).Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        public void ApplySort(IComparer<T> comparer)
        {
            List<T> items = this.Items as List<T>;
            if (items != null)
            {
                items.Sort(comparer);
            }
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        public int Find(PropertyDescriptor property, object key)
        {
            return FindCore(property, key);
        }
    }
}
