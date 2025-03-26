using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Business.Models;
using Repositories;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class RoomInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly GenericRepository<RoomInformation> _roomRepo;
        public ObservableCollection<RoomInformation> RoomInformations { get; set; }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public RoomInformationViewModel()
        {
            _roomRepo = new GenericRepository<RoomInformation>(new Business.Models.FuminiHotelManagementContext());
            RoomInformations = new ObservableCollection<RoomInformation>(_roomRepo.GetAll());

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

        private void ExecuteCreate(object? parameter)
        {
            var newRoom = new RoomInformation
            {
                RoomId = RoomInformations.Count + 1,
                RoomNumber = RoomNumber,
                RoomDetailDescription = RoomDetailDescription,
                RoomMaxCapacity = RoomMaxCapacity,
                RoomTypeId = RoomTypeId,
                RoomStatus = RoomStatus,
                RoomPricePerDay = RoomPricePerDay
            };

            _roomRepo.Add(newRoom);
            RoomInformations.Add(newRoom);
            ExecuteClear(null);
        }

        private bool CanExecuteCreate(object? parameter) => !string.IsNullOrWhiteSpace(RoomNumber);

        private void ExecuteUpdate(object? parameter)
        {
            var existingRoom = _roomRepo.GetById(RoomId);
            if (existingRoom != null)
            {
                existingRoom.RoomNumber = RoomNumber;
                existingRoom.RoomDetailDescription = RoomDetailDescription;
                existingRoom.RoomMaxCapacity = RoomMaxCapacity;
                existingRoom.RoomTypeId = RoomTypeId;
                existingRoom.RoomStatus = RoomStatus;
                existingRoom.RoomPricePerDay = RoomPricePerDay;

                _roomRepo.Update(existingRoom);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteUpdate(object? parameter) => RoomId > 0;

        private void ExecuteDelete(object? parameter)
        {
            _roomRepo.Delete(RoomId);
            var roomToRemove = RoomInformations.FirstOrDefault(r => r.RoomId == RoomId);
            if (roomToRemove != null)
            {
                RoomInformations.Remove(roomToRemove);
                ExecuteClear(null);
            }
        }

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
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
