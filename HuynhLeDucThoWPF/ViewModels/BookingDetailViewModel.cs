using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Linq.Expressions;
using Business.Models;
using Repositories;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class BookingDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly GenericRepository<BookingDetail> _bookingDetailRepo;
        public ObservableCollection<BookingDetail> BookingDetails { get; set; }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public BookingDetailViewModel()
        {
            _bookingDetailRepo = new GenericRepository<BookingDetail>(new Business.Models.FuminiHotelManagementContext());
            BookingDetails = new ObservableCollection<BookingDetail>(_bookingDetailRepo.GetAll());

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

        private void ExecuteCreate(object? parameter)
        {
            var newBookingDetail = new BookingDetail
            {
                BookingReservationId = BookingReservationId,
                RoomId = RoomId,
                StartDate = StartDate,
                EndDate = EndDate,
                ActualPrice = ActualPrice
            };

            _bookingDetailRepo.Add(newBookingDetail);
            BookingDetails.Add(newBookingDetail);
            ExecuteClear(null);
        }

        private bool CanExecuteCreate(object? parameter) => BookingReservationId > 0 && RoomId > 0;

        private void ExecuteUpdate(object? parameter)
        {
            Expression<Func<BookingDetail, bool>> filter = bd =>
                bd.BookingReservationId == BookingReservationId && bd.RoomId == RoomId;

            var existingBookingDetail = _bookingDetailRepo.FindByCondition(filter).FirstOrDefault();
            if (existingBookingDetail != null)
            {
                existingBookingDetail.StartDate = StartDate;
                existingBookingDetail.EndDate = EndDate;
                existingBookingDetail.ActualPrice = ActualPrice;

                _bookingDetailRepo.Update(existingBookingDetail);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteUpdate(object? parameter) => BookingReservationId > 0 && RoomId > 0;

        private void ExecuteDelete(object? parameter)
        {
            Expression<Func<BookingDetail, bool>> filter = bd =>
                bd.BookingReservationId == BookingReservationId && bd.RoomId == RoomId;

            var detailToRemove = _bookingDetailRepo.Fi(filter).FirstOrDefault();
            if (detailToRemove != null)
            {
                _bookingDetailRepo.Delete(detailToRemove);
                BookingDetails.Remove(detailToRemove);
                ExecuteClear(null);
            }
        }

        private bool CanExecuteDelete(object? parameter) => BookingReservationId > 0 && RoomId > 0;

        private void ExecuteClear(object? parameter)
        {
            BookingReservationId = 0;
            RoomId = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            ActualPrice = null;
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
