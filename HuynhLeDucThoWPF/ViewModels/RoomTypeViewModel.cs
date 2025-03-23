using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Business.Models;

namespace HotelManagementApp.ViewModels
{
    public class RoomTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public RoomTypeViewModel()
        {
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

        private void ExecuteCreate(object? parameter) => Console.WriteLine("Room Type Created: " + RoomTypeName);
        private bool CanExecuteCreate(object? parameter) => !string.IsNullOrWhiteSpace(RoomTypeName);
        private void ExecuteUpdate(object? parameter) => Console.WriteLine("Room Type Updated: " + RoomTypeId);
        private bool CanExecuteUpdate(object? parameter) => RoomTypeId > 0;
        private void ExecuteDelete(object? parameter) => Console.WriteLine("Room Type Deleted: " + RoomTypeId);
        private bool CanExecuteDelete(object? parameter) => RoomTypeId > 0;
        private void ExecuteClear(object? parameter)
        {
            RoomTypeId = 0;
            RoomTypeName = string.Empty;
            TypeDescription = string.Empty;
            TypeNote = string.Empty;
            Console.WriteLine("Room Type Form Cleared");
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
