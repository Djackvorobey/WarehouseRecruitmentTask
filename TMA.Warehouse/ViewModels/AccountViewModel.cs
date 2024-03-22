using System.ComponentModel;
using static TMA.Warehouse.Models.Enums.Enums;

namespace TMA.Warehouse.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private RoleEnums _roleEnum;
        public RoleEnums RoleEnum
        {
            get { return _roleEnum; }
            set
            {
                _roleEnum = value;
                OnPropertyChanged(nameof(RoleEnum));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
