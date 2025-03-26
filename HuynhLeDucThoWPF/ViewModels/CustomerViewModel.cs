using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;
using Repositories;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly GenericRepository<Customer> _customerRepo;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public ObservableCollection<Customer> Customers { get; set; }

        public CustomerViewModel()
        {
            _customerRepo = new GenericRepository<Customer>(new Business.Models.FuminiHotelManagementContext());

            Customers = new ObservableCollection<Customer>(_customerRepo.GetAll());

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
            var newCustomer = new Customer
            {
                CustomerId = Customers.Count + 1,
                CustomerFullName = CustomerFullName,
                Telephone = Telephone,
                EmailAddress = EmailAddress,
                CustomerBirthday = CustomerBirthday,
                CustomerStatus = CustomerStatus,
                Password = Password
            };

            _customerRepo.Add(newCustomer);
            Customers.Add(newCustomer);
            ExecuteClear(null);
        }

        private bool CanExecuteCreate(object? parameter) => !string.IsNullOrWhiteSpace(CustomerFullName) && !string.IsNullOrWhiteSpace(EmailAddress);

        private void ExecuteUpdate(object? parameter)
        {
            var existingCustomer = _customerRepo.GetById(CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerFullName = CustomerFullName;
                existingCustomer.Telephone = Telephone;
                existingCustomer.EmailAddress = EmailAddress;
                existingCustomer.CustomerBirthday = CustomerBirthday;
                existingCustomer.CustomerStatus = CustomerStatus;
                existingCustomer.Password = Password;

                _customerRepo.Update(existingCustomer);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteUpdate(object? parameter) => CustomerId > 0;

        private void ExecuteDelete(object? parameter)
        {
            _customerRepo.Delete(CustomerId);
            var customerToRemove = Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
            if (customerToRemove != null)
            {
                Customers.Remove(customerToRemove);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteDelete(object? parameter) => CustomerId > 0;

        private void ExecuteClear(object? parameter)
        {
            CustomerId = 0;
            CustomerFullName = string.Empty;
            Telephone = string.Empty;
            EmailAddress = string.Empty;
            CustomerBirthday = null;
            CustomerStatus = null;
            Password = string.Empty;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
