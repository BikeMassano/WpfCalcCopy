using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Interfaces;
using WpfApp1.Models.Operations;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int thresholdWidth = 600;
        public MainWindow()
        {

            DataContext = new CalculatorViewModel(new Calculator());
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}