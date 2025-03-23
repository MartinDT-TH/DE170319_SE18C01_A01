using System;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelManagementApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private string _emailAddress = string.Empty;
        private string _password = string.Empty;
        private bool _isLoginSuccessful;

        public string EmailAddress
        {
            get => _emailAddress;
            set { _emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public bool IsLoginSuccessful
        {
            get => _isLoginSuccessful;
            set { _isLoginSuccessful = value; OnPropertyChanged(nameof(IsLoginSuccessful)); }
        }

        private void ExecuteLogin(object? parameter)
        {
            // Simulating login authentication
            if (EmailAddress == "admin@example.com" && Password == "admin123")
            {
                IsLoginSuccessful = true;
                Console.WriteLine("Login Successful!");
            }
            else
            {
                IsLoginSuccessful = false;
                Console.WriteLine("Invalid Credentials!");
            }
        }

        private bool CanExecuteLogin(object? parameter) => 
            !string.IsNullOrWhiteSpace(EmailAddress) && !string.IsNullOrWhiteSpace(Password);

        private void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
