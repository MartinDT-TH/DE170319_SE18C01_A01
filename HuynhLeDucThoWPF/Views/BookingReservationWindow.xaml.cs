using System.Windows;
using HuynhLeDucThoWPF.ViewModels;

namespace HuynhLeDucThoWPF.Views
{
    public partial class BookingReservationWindow : Window
    {
        private readonly BookingReservationViewModel _viewModel;

        public BookingReservationWindow()
        {
            InitializeComponent();
            _viewModel = new BookingReservationViewModel();
            DataContext = _viewModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SaveCommand.CanExecute(null))
            {
                _viewModel.SaveCommand.Execute(null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
