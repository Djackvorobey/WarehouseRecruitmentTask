using System.ComponentModel;

namespace TMA.Warehouse.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _itemGroup;
        public string ItemGroup
        {
            get { return _itemGroup; }
            set
            {
                _itemGroup = value;
                OnPropertyChanged("ItemGroup");
            }
        }

        private string _unitOfMeasurement;
        public string UnitOfMeasurement
        {
            get { return _unitOfMeasurement; }
            set
            {
                _unitOfMeasurement = value;
                OnPropertyChanged("UnitOfMeasurement");
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private decimal _priceWithoutVAT;
        public decimal PriceWithoutVAT
        {
            get { return _priceWithoutVAT; }
            set
            {
                _priceWithoutVAT = value;
                OnPropertyChanged("PriceWithoutVAT");
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private string _storageLocation;
        public string StorageLocation
        {
            get { return _storageLocation; }
            set
            {
                _storageLocation = value;
                OnPropertyChanged("StorageLocation");
            }
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get { return _contactPerson; }
            set
            {
                _contactPerson = value;
                OnPropertyChanged("ContactPerson");
            }
        }

        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

