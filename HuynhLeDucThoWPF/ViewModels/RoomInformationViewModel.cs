using System;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;

namespace HotelManagementApp.ViewModels
{
    public class RoomInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public RoomInformationViewModel()
        {
            CreateCommand = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _roomId;
        private string _roomNumber = string.Empty;
        private string? _roomDetailDescription;
        private int? _roomMaxCapacity;
        private int _roomTypeId;
        private byte? _roomStatus;
        private decimal? _roomPricePerDay;

        public int RoomId
        {
            get => _roomId;
            set { _roomId = value; OnPropertyChanged(nameof(RoomId)); }
        }

        public string RoomNumber
        {
            get => _roomNumber;
            set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        }

        public string? RoomDetailDescription
        {
            get => _roomDetailDescription;
            set { _roomDetailDescription = value; OnPropertyChanged(nameof(RoomDetailDescription)); }
        }

        public int? RoomMaxCapacity
        {
            get => _roomMaxCapacity;
            set { _roomMaxCapacity = value; OnPropertyChanged(nameof(RoomMaxCapacity)); }
        }

        public int RoomTypeId
        {
            get => _roomTypeId;
            set { _roomTypeId = value; OnPropertyChanged(nameof(RoomTypeId)); }
        }

        public byte? RoomStatus
        {
            get => _roomStatus;
            set { _roomStatus = value; OnPropertyChanged(nameof(RoomStatus)); }
        }

        public decimal? RoomPricePerDay
        {
            get => _roomPricePerDay;
            set { _roomPricePerDay = value; OnPropertyChanged(nameof(RoomPricePerDay)); }
        }

        private void ExecuteCreate(object? parameter) => Console.WriteLine("Room Created: " + RoomNumber);
        private bool CanExecuteCreate(object? parameter) => !string.IsNullOrWhiteSpace(RoomNumber);
        private void ExecuteUpdate(object? parameter) => Console.WriteLine("Room Updated: " + RoomId);
        private bool CanExecuteUpdate(object? parameter) => RoomId > 0;
        private void ExecuteDelete(object? parameter) => Console.WriteLine("Room Deleted: " + RoomId);
        private bool CanExecuteDelete(object? parameter) => RoomId > 0;
        private void ExecuteClear(object? parameter)
        {
            RoomId = 0;
            RoomNumber = string.Empty;
            RoomDetailDescription = string.Empty;
            RoomMaxCapacity = null;
            RoomTypeId = 0;
            RoomStatus = null;
            RoomPricePerDay = null;
            Console.WriteLine("Room Form Cleared");
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
