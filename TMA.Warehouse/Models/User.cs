using System.ComponentModel;

namespace TMA.Warehouse.Models
{
    public class User
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private Enums.Enums.RoleEnums _roleEnum;
        public Enums.Enums.RoleEnums RoleEnum
        {
            get { return _roleEnum; }
            set
            {
                if (_roleEnum != value)
                {
                    _roleEnum = value;
                    OnPropertyChanged(nameof(RoleEnum));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
