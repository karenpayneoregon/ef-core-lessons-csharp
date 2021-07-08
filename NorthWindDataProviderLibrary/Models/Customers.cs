using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NorthWindDataProviderLibrary.Models
{
    /// <summary>
    /// INotifyPropertyChanged Notifies clients that a property value has changed.
    /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-5.0
    /// </summary>
    public class Customers :   INotifyPropertyChanged
    {
        private string _companyName;
        private int? _contactId;
        private string _street;
        private string _city;
        private string _region;
        private string _postalCode;
        private int? _countryIdentifier;
        private string _phone;
        private string _fax;
        private int? _contactTypeIdentifier;
        private DateTime? _modifiedDate;
        public int CustomerIdentifier { get; set; }

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        public int? ContactId
        {
            get => _contactId;
            set
            {
                _contactId = value;
                OnPropertyChanged();
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string Region
        {
            get => _region;
            set
            {
                _region = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        public int? CountryIdentifier
        {
            get => _countryIdentifier;
            set
            {
                _countryIdentifier = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string Fax
        {
            get => _fax;
            set
            {
                _fax = value;
                OnPropertyChanged();
            }
        }

        public int? ContactTypeIdentifier
        {
            get => _contactTypeIdentifier;
            set
            {
                _contactTypeIdentifier = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                _modifiedDate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}