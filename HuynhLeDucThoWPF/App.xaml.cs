using System.Windows;
using Business.Models;
using HuynhLeDucThoWPF.ViewModels;
using HuynhLeDucThoWPF.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace HuynhLeDucThoWPF
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();

            // Register DbContext with In-Memory Database
            services.AddDbContext<FuminiHotelManagementContext>(options =>
                options.UseInMemoryDatabase("FUMiniHotelManagement"));

            services.AddSingleton<FuminiHotelManagementContext>(); // Acts as in-memory database


            // Register AuthService and Authentication Services
            services.AddSingleton<IAuthService, AuthService>();
            services.AddTransient<LoginWindow>();

            // Register Repositories (Scoped for DB operations)
            services.AddScoped<IRepository<BookingReservation>, GenericRepository<BookingReservation>>();

            // Register ViewModels
            services.AddTransient<BookingReservationViewModel>();

            // Register Views
            services.AddTransient<BookingReservationWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}

