using System.Windows;
using HuynhLeDucThoWPF.ViewModels;

namespace HuynhLeDucThoWPF.Views
{
    public partial class BookingReservationWindow : Window
    {
        private readonly BookingReservationViewModel _viewModel;

        public BookingReservationWindow(BookingReservationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            DataContext = _viewModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CreateCommand != null && _viewModel.CreateCommand.CanExecute(null))
            {
                _viewModel.CreateCommand.Execute(null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
