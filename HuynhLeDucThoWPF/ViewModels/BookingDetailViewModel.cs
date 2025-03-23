using System;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;

namespace HotelManagementApp.ViewModels
{
    public class BookingDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public BookingDetailViewModel()
        {
            CreateCommand = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _bookingReservationId;
        private int _roomId;
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today;
        private decimal? _actualPrice;

        public int BookingReservationId
        {
            get => _bookingReservationId;
            set { _bookingReservationId = value; OnPropertyChanged(nameof(BookingReservationId)); }
        }

        public int RoomId
        {
            get => _roomId;
            set { _roomId = value; OnPropertyChanged(nameof(RoomId)); }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        public decimal? ActualPrice
        {
            get => _actualPrice;
            set { _actualPrice = value; OnPropertyChanged(nameof(ActualPrice)); }
        }

        private void ExecuteCreate(object? parameter) => Console.WriteLine("Booking Detail Created");
        private bool CanExecuteCreate(object? parameter) => BookingReservationId > 0 && RoomId > 0;
        private void ExecuteUpdate(object? parameter) => Console.WriteLine("Booking Detail Updated");
        private bool CanExecuteUpdate(object? parameter) => BookingReservationId > 0 && RoomId > 0;
        private void ExecuteDelete(object? parameter) => Console.WriteLine("Booking Detail Deleted");
        private bool CanExecuteDelete(object? parameter) => BookingReservationId > 0 && RoomId > 0;
        private void ExecuteClear(object? parameter)
        {
            BookingReservationId = 0;
            RoomId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            ActualPrice = null;
            Console.WriteLine("Booking Detail Form Cleared");
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
