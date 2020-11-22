﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfAppCore
{
    public class RESTResult<T> : INotifyPropertyChanged where T:class
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        private T _selectedItem;

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        private ObservableCollection<T> _items = new ObservableCollection<T>();

        // Replacing collection does not fire PCN.  Need to raise ourselves.

        //public ObservableCollection<T> ResultItems
        //{
        //    get => _items;
        //    set => _items = value;
        //}

        public ObservableCollection<T> ResultItems
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultItems)));
            }
        }
    }
}
