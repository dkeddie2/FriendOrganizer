using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FriendOrganizer.Model;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendWrapper : ViewModelBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errorsByPropertyName = new Dictionary<string, List<string>>();

        public FriendWrapper(Friend model)
        {
            Model = model;
        }

        public Friend Model { get; }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return Model.FirstName; }
            set
            {
                Model.FirstName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return Model.Email; }
            set
            {
                Model.Email = value;
                OnPropertyChanged();
            }
        }

        void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        AddError(propertyName, "Robots are not valid friends");
                    }
                    break;
            }
        }

        public bool HasErrors => errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorsByPropertyName.ContainsKey(propertyName) ? errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string propertyName, string error)
        {
            if (errorsByPropertyName.ContainsKey(propertyName) == false)
            {
                errorsByPropertyName[propertyName] = new List<string>();
            }
            if (errorsByPropertyName[propertyName].Contains(error) == false)
            {
                errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (errorsByPropertyName.ContainsKey(propertyName))
            {
                errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
