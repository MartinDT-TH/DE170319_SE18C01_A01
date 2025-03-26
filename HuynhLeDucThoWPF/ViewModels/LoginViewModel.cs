using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IAuthService _authService;

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            LoginCommand = new RelayCommand(async (_) => await ExecuteLogin(), CanExecuteLogin);
        }

        private string _emailAddress = string.Empty;
        private string _password = string.Empty;
        private bool _isLoginSuccessful;
        private string _errorMessage = string.Empty;

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

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        private async Task ExecuteLogin()
        {
            try
            {
                bool loginResult = await _authService.AuthenticateAsync(EmailAddress, Password);

                if (loginResult)
                {
                    IsLoginSuccessful = true;
                    ErrorMessage = string.Empty;
                    Console.WriteLine("Login Successful!");
                }
                else
                {
                    IsLoginSuccessful = false;
                    ErrorMessage = "Invalid email or password. Please try again.";
                    Password = string.Empty;
                    Console.WriteLine("Invalid Credentials!");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during login. Please try again later.";
                Console.WriteLine($"Login Error: {ex.Message}");
            }
        }

        private bool CanExecuteLogin(object? parameter) =>
            !string.IsNullOrWhiteSpace(EmailAddress) && !string.IsNullOrWhiteSpace(Password);

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
