﻿using System;
using System.ComponentModel;

namespace Business
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Properties

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }       

        private DateTime? _lastUpdated;
        public DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                _lastUpdated = value;
                OnPropertyChanged("LastUpdated");
            }
        }

        #endregion //Properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged

        #region IDataErrorInfo

        private string _Error;
        public string Error
        {
            get { return _Error; }
            private set
            {
                _Error = value;
                OnPropertyChanged("Error");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(_firstName))
                        {
                            error = "First Name required";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(_lastName))
                        {
                            error = "Last Name required";
                        }
                        break;
                    case "Age":
                        if ((_age < 18) || (_age > 85))
                        {
                            error = "Age out of range.";
                        }

                        break;
                }
                Error = error;
                return (Error);
            }
        }

        #endregion //IDataErrorInfo

        public override string ToString()
        {
            return String.Format("{0}, {1}", FirstName, LastName);
        }
    }
}
