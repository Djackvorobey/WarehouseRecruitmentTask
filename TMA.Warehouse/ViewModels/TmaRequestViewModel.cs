using System.ComponentModel;

namespace TMA.Warehouse.ViewModels
{
    public class TmaRequestViewModel : INotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set
            {
                if (_itemId != value)
                {
                    _itemId = value;
                    OnPropertyChanged(nameof(ItemId));
                }
            }
        }

        private string _unitOfMeasurement;
        public string UnitOfMeasurement
        {
            get { return _unitOfMeasurement; }
            set
            {
                if (_unitOfMeasurement != value)
                {
                    _unitOfMeasurement = value;
                    OnPropertyChanged(nameof(UnitOfMeasurement));
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private decimal _priceWithoutVAT;
        public decimal PriceWithoutVAT
        {
            get { return _priceWithoutVAT; }
            set
            {
                if (_priceWithoutVAT != value)
                {
                    _priceWithoutVAT = value;
                    OnPropertyChanged(nameof(PriceWithoutVAT));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        private int _requestRowId;
        public int RequestRowId
        {
            get { return _requestRowId; }
            set
            {
                if (_requestRowId != value)
                {
                    _requestRowId = value;
                    OnPropertyChanged(nameof(RequestRowId));
                }
            }
        }

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }

        private string _itemGroup;
        public string ItemGroup
        {
            get { return _itemGroup; }
            set
            {
                _itemGroup = value;
                OnPropertyChanged(nameof(ItemGroup));
            }
        }

        private int _itemQuantity;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                OnPropertyChanged(nameof(ItemQuantity));
            }
        }

        private string _storageLocation;
        public string StorageLocation
        {
            get { return _storageLocation; }
            set
            {
                _storageLocation = value;
                OnPropertyChanged(nameof(StorageLocation));
            }
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get { return _contactPerson; }
            set
            {
                _contactPerson = value;
                OnPropertyChanged(nameof(ContactPerson));
            }
        }

        private string _itemPhoto;
        public string ItemPhoto
        {
            get { return _itemPhoto; }
            set
            {
                _itemPhoto = value;
                OnPropertyChanged(nameof(ItemPhoto));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
