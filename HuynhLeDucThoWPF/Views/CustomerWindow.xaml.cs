using System.Windows;
using HuynhLeDucThoWPF.ViewModels;

namespace HuynhLeDucThoWPF.Views
{
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel();
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
