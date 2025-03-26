using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Business.Models;
using Repositories;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class BookingReservationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IRepository<BookingReservation> _bookingRepo;

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public BookingReservationViewModel(GenericRepository<BookingReservation> bookingRepo)
        {
            _bookingRepo = bookingRepo ?? throw new ArgumentNullException(nameof(bookingRepo));

            CreateCommand = new RelayCommand(async (_) => await ExecuteCreate(), CanExecuteCreate);
            UpdateCommand = new RelayCommand(async (_) => await ExecuteUpdate(), CanExecuteUpdate);
            DeleteCommand = new RelayCommand(async (_) => await ExecuteDelete(), CanExecuteDelete);
            ClearCommand = new RelayCommand(ExecuteClear);
        }

        private int _bookingReservationId;
        private DateOnly? _bookingDate;
        private decimal? _totalPrice;
        private int _customerId;
        private byte? _bookingStatus;

        public int BookingReservationId
        {
            get => _bookingReservationId;
            set { _bookingReservationId = value; OnPropertyChanged(nameof(BookingReservationId)); }
        }

        public DateOnly? BookingDate
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

        private async Task ExecuteCreate()
        {
            var newBooking = new BookingReservation
            {
                BookingDate = DateTime.Now,
                TotalPrice = TotalPrice ?? 0,
                CustomerId = CustomerId,
                BookingStatus = BookingStatus ?? 1 // Assuming 1 = Active
            };

            var createdBooking = await _bookingRepo.AddAsync(newBooking);
            if (createdBooking is not null)
            {
                ExecuteClear();
                Console.WriteLine("✅ Booking Created Successfully!");
            }
        }

        private bool CanExecuteCreate(object? parameter) => CustomerId > 0;

        private async Task ExecuteUpdate()
        {
            if (BookingReservationId > 0)
            {
                var existingBooking = await _bookingRepo.GetByIdAsync(BookingReservationId);
                if (existingBooking is not null)
                {
                    existingBooking.BookingDate = BookingDate;
                    existingBooking.TotalPrice = TotalPrice;
                    existingBooking.CustomerId = CustomerId;
                    existingBooking.BookingStatus = BookingStatus;

                    var updatedBooking = await _bookingRepo.UpdateAsync(existingBooking);
                    if (updatedBooking is not null)
                    {
                        ExecuteClear();
                        Console.WriteLine("✅ Booking Updated Successfully!");
                    }
                }
            }
        }

        private bool CanExecuteUpdate(object? parameter) => BookingReservationId > 0;

        private async Task ExecuteDelete()
        {
            if (BookingReservationId > 0)
            {
                bool isDeleted = await _bookingRepo.DeleteAsync(BookingReservationId);
                if (isDeleted)
                {
                    ExecuteClear();
                    Console.WriteLine("❌ Booking Deleted Successfully!");
                }
            }
        }

        private bool CanExecuteDelete(object? parameter) => BookingReservationId > 0;

        private void ExecuteClear(object? parameter = null)
        {
            BookingReservationId = 0;
            BookingDate = null;
            TotalPrice = null;
            CustomerId = 0;
            BookingStatus = null;

            Console.WriteLine("🧹 Booking Form Cleared");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

