using System.Windows;
using HuynhLeDucThoWPF.ViewModels;

namespace HuynhLeDucThoWPF.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(IAuthService authService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authService); // ✅ Corrected instantiation
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            if (((LoginViewModel)DataContext).IsLoginSuccessful)
            {
                this.DialogResult = true; // ✅ Close and return success
                this.Close();
            }
        }
    }
}
