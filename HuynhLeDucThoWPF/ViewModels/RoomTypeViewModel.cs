using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Business.Models;
using Repositories;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class RoomTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly GenericRepository<RoomType> _roomTypeRepo;
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public RoomTypeViewModel()
        {
            _roomTypeRepo = new GenericRepository<RoomType>(new Business.Models.FuminiHotelManagementContext());
            RoomTypes = new ObservableCollection<RoomType>(_roomTypeRepo.GetAll());

            CreateCommand = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _roomTypeId;
        private string _roomTypeName = string.Empty;
        private string? _typeDescription;
        private string? _typeNote;

        public int RoomTypeId
        {
            get => _roomTypeId;
            set { _roomTypeId = value; OnPropertyChanged(nameof(RoomTypeId)); }
        }

        public string RoomTypeName
        {
            get => _roomTypeName;
            set { _roomTypeName = value; OnPropertyChanged(nameof(RoomTypeName)); }
        }

        public string? TypeDescription
        {
            get => _typeDescription;
            set { _typeDescription = value; OnPropertyChanged(nameof(TypeDescription)); }
        }

        public string? TypeNote
        {
            get => _typeNote;
            set { _typeNote = value; OnPropertyChanged(nameof(TypeNote)); }
        }

        private void ExecuteCreate(object? parameter)
        {
            var newRoomType = new RoomType
            {
                RoomTypeId = RoomTypes.Count + 1,
                RoomTypeName = RoomTypeName,
                TypeDescription = TypeDescription,
                TypeNote = TypeNote
            };

            _roomTypeRepo.Add(newRoomType);
            RoomTypes.Add(newRoomType);
            ExecuteClear(null);
        }

        private bool CanExecuteCreate(object? parameter) => !string.IsNullOrWhiteSpace(RoomTypeName);

        private void ExecuteUpdate(object? parameter)
        {
            var existingRoomType = _roomTypeRepo.GetById(RoomTypeId);
            if (existingRoomType != null)
            {
                existingRoomType.RoomTypeName = RoomTypeName;
                existingRoomType.TypeDescription = TypeDescription;
                existingRoomType.TypeNote = TypeNote;

                _roomTypeRepo.Update(existingRoomType);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteUpdate(object? parameter) => RoomTypeId > 0;

        private void ExecuteDelete(object? parameter)
        {
            _roomTypeRepo.Delete(RoomTypeId);
            var roomTypeToRemove = RoomTypes.FirstOrDefault(r => r.RoomTypeId == RoomTypeId);
            if (roomTypeToRemove != null)
            {
                RoomTypes.Remove(roomTypeToRemove);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteDelete(object? parameter) => RoomTypeId > 0;

        private void ExecuteClear(object? parameter)
        {
            RoomTypeId = 0;
            RoomTypeName = string.Empty;
            TypeDescription = string.Empty;
            TypeNote = string.Empty;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
