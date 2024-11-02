using ConsoleCalculator.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Interfaces;
using WpfApp1.Models.Operations;
using WpfApp1.MVVM;
using WpfApp1.Services;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int thresholdWidth = 500;
        public MainWindow()
        {
            var services = new ServiceCollection();

            // Регистрация операций
            services.AddTransient<IOperation, AddOperation>();
            services.AddTransient<IOperation, SubtractOperation>();
            services.AddTransient<IOperation, MultiplyOperation>();
            services.AddTransient<IOperation, DivideOperation>();
            services.AddTransient<IOperation, PowerOperation>();
            services.AddTransient<IOperation, ModulusOperation>();
            services.AddTransient<IOperation, SqrtOperation>();

            // Регистрация других сервисов
            services.AddTransient<ITokenizer, Tokenizer>();
            services.AddTransient<IParser, Parser>();
            services.AddTransient<IEvaluator, Evaluator>();
            services.AddTransient<Calculator>();

            var serviceProvider = services.BuildServiceProvider();

            var calculator = serviceProvider.GetService<Calculator>();

            DataContext = new CalculatorViewModel(calculator);

            // Подписка на событие изменения размеров окна
            this.SizeChanged += Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Получите ссылку на MyGrid
            Grid MyGrid = (Grid)this.FindName("MyGrid");
            if (MyGrid != null)
            {
                ColumnDefinition secondColumn = (ColumnDefinition)MyGrid.ColumnDefinitions[1];

                if (e.NewSize.Width >= thresholdWidth)
                {
                    secondColumn.Width = new GridLength(0.75, GridUnitType.Star);
                }
                else
                {
                    secondColumn.Width = new GridLength(0);
                }
            }
        }
    }
}