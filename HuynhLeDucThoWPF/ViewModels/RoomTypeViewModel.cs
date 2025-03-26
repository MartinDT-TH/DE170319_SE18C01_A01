using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Business.Models;
using HuynhLeDucThoWPF.Repositories;

namespace HotelManagementApp.ViewModels
{
    public class RoomTypeViewModel : INotifyPropertyChanged
    {
        private readonly RoomTypeRepository _roomTypeRepo;
        private ObservableCollection<RoomType> _roomTypes;
        private RoomType? _selectedRoomType;

        public RoomTypeViewModel()
        {
            _roomTypeRepo = new RoomTypeRepository();
            _roomTypes = new ObservableCollection<RoomType>(_roomTypeRepo.GetAllRoomTypes());

            CreateCommand = new RelayCommand(async (_) => await ExecuteCreate(), CanExecuteCreate);
            UpdateCommand = new RelayCommand(async (_) => await ExecuteUpdate(), CanExecuteUpdate);
            DeleteCommand = new RelayCommand(async (_) => await ExecuteDelete(), CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set { _roomTypes = value; OnPropertyChanged(); }
        }

        public RoomType? SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                if (value != null)
                {
                    RoomTypeId = value.RoomTypeId;
                    RoomTypeName = value.RoomTypeName;
                    TypeDescription = value.TypeDescription;
                    TypeNote = value.TypeNote;
                }
                OnPropertyChanged();
            }
        }

        private int _roomTypeId;
        private string _roomTypeName = string.Empty;
        private string? _typeDescription;
        private string? _typeNote;

        public int RoomTypeId
        {
            get => _roomTypeId;
            set { _roomTypeId = value; OnPropertyChanged(); }
        }

        public string RoomTypeName
        {
            get => _roomTypeName;
            set { _roomTypeName = value; OnPropertyChanged(); }
        }

        public string? TypeDescription
        {
            get => _typeDescription;
            set { _typeDescription = value; OnPropertyChanged(); }
        }

        public string? TypeNote
        {
            get => _typeNote;
            set { _typeNote = value; OnPropertyChanged(); }
        }

        private async Task ExecuteCreate()
        {
            var newRoomType = new RoomType
            {
                RoomTypeName = RoomTypeName,
                TypeDescription = TypeDescription,
                TypeNote = TypeNote
            };

            if (_roomTypeRepo.AddRoomType(newRoomType))
            {
                RoomTypes.Add(newRoomType);
                ExecuteClear();
                Console.WriteLine("Room Type Created Successfully!");
            }
        }

        private bool CanExecuteCreate(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(RoomTypeName);
        }

        private async Task ExecuteUpdate()
        {
            if (SelectedRoomType != null)
            {
                SelectedRoomType.RoomTypeName = RoomTypeName;
                SelectedRoomType.TypeDescription = TypeDescription;
                SelectedRoomType.TypeNote = TypeNote;

                if (_roomTypeRepo.UpdateRoomType(SelectedRoomType))
                {
                    ExecuteClear();
                    Console.WriteLine("Room Type Updated Successfully!");
                }
            }
        }

        private bool CanExecuteUpdate(object? parameter)
        {
            return RoomTypeId > 0;
        }

        private async Task ExecuteDelete()
        {
            if (SelectedRoomType != null)
            {
                if (_roomTypeRepo.DeleteRoomType(SelectedRoomType.RoomTypeId))
                {
                    RoomTypes.Remove(SelectedRoomType);
                    ExecuteClear();
                    Console.WriteLine("Room Type Deleted Successfully!");
                }
            }
        }

        private bool CanExecuteDelete(object? parameter)
        {
            return RoomTypeId > 0;
        }

        private void ExecuteClear(object? parameter = null)
        {
            RoomTypeId = 0;
            RoomTypeName = string.Empty;
            TypeDescription = string.Empty;
            TypeNote = string.Empty;
            SelectedRoomType = null;
            Console.WriteLine("Room Type Form Cleared");
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
