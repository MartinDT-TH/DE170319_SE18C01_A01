using System;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;

namespace HotelManagementApp.ViewModels
{
    public class BookingReservationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public BookingReservationViewModel()
        {
            CreateCommand = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _bookingReservationId;
        private DateTime? _bookingDate;
        private decimal? _totalPrice;
        private int _customerId;
        private byte? _bookingStatus;

        public int BookingReservationId
        {
            get => _bookingReservationId;
            set { _bookingReservationId = value; OnPropertyChanged(nameof(BookingReservationId)); }
        }

        public DateTime? BookingDate
        {
            get => _bookingDate;
            set { _bookingDate = value; OnPropertyChanged(nameof(BookingDate)); }
        }

        public decimal? TotalPrice
        {
            get => _totalPrice;
            set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public int CustomerId
        {
            get => _customerId;
            set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
        }

        public byte? BookingStatus
        {
            get => _bookingStatus;
            set { _bookingStatus = value; OnPropertyChanged(nameof(BookingStatus)); }
        }

        private void ExecuteCreate(object? parameter) => Console.WriteLine("Booking Created");
        private bool CanExecuteCreate(object? parameter) => CustomerId > 0;
        private void ExecuteUpdate(object? parameter) => Console.WriteLine("Booking Updated");
        private bool CanExecuteUpdate(object? parameter) => BookingReservationId > 0;
        private void ExecuteDelete(object? parameter) => Console.WriteLine("Booking Deleted");
        private bool CanExecuteDelete(object? parameter) => BookingReservationId > 0;
        private void ExecuteClear(object? parameter) => Console.WriteLine("Booking Form Cleared");

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
