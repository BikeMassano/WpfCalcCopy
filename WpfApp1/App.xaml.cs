using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp1.Interfaces;
using WpfApp1.Models.Operations;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            _serviceProvider = ServiceConfigurator.ConfigureServices();  // Инициализация зависимостей
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Получаем экземпляр калькулятора и создаём ViewModel
            var calculator = _serviceProvider.GetService<ICalculator>();
            var viewModel = new CalculatorViewModel(calculator);

            // Создаём MainWindow и передаём ViewModel
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }
    }


}
