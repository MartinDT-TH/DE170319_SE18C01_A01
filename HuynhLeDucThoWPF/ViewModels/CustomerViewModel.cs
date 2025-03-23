using System;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;

namespace HotelManagementApp.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public CustomerViewModel()
        {
            CreateCommand = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _customerId;
        private string? _customerFullName;
        private string? _telephone;
        private string _emailAddress = string.Empty;
        private DateTime? _customerBirthday;
        private byte? _customerStatus;
        private string? _password;

        public int CustomerId
        {
            get => _customerId;
            set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
        }

        public string? CustomerFullName
        {
            get => _customerFullName;
            set { _customerFullName = value; OnPropertyChanged(nameof(CustomerFullName)); }
        }

        public string? Telephone
        {
            get => _telephone;
            set { _telephone = value; OnPropertyChanged(nameof(Telephone)); }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set { _emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); }
        }

        public DateTime? CustomerBirthday
        {
            get => _customerBirthday;
            set { _customerBirthday = value; OnPropertyChanged(nameof(CustomerBirthday)); }
        }

        public byte? CustomerStatus
        {
            get => _customerStatus;
            set { _customerStatus = value; OnPropertyChanged(nameof(CustomerStatus)); }
        }

        public string? Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private void ExecuteCreate(object? parameter)
        {
            // Logic to add a new customer (e.g., save to database)
            Console.WriteLine("Customer Created: " + CustomerFullName);
        }

        private bool CanExecuteCreate(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(CustomerFullName) &&
                   !string.IsNullOrWhiteSpace(EmailAddress);
        }

        private void ExecuteUpdate(object? parameter)
        {
            // Logic to update an existing customer
            Console.WriteLine("Customer Updated: " + CustomerId);
        }

        private bool CanExecuteUpdate(object? parameter)
        {
            return CustomerId > 0; // Ensure customer exists before updating
        }

        private void ExecuteDelete(object? parameter)
        {
            // Logic to delete customer
            Console.WriteLine("Customer Deleted: " + CustomerId);
        }

        private bool CanExecuteDelete(object? parameter)
        {
            return CustomerId > 0; // Ensure customer exists before deleting
        }

        private void ExecuteClear(object? parameter)
        {
            // Clears the form fields
            CustomerId = 0;
            CustomerFullName = string.Empty;
            Telephone = string.Empty;
            EmailAddress = string.Empty;
            CustomerBirthday = null;
            CustomerStatus = null;
            Password = string.Empty;

            Console.WriteLine("Form Cleared");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
