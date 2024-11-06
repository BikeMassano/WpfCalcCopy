using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Обработка нажатия клавиш
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                // Цифры от 0 до 9
                ((CalculatorViewModel)DataContext).EnterNumberCommand.Execute(e.Key.ToString().Substring(1));
            }
            else if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 ||
                     e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 ||
                     e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                     e.Key == Key.NumPad9)
            {
                // Цифры на NumPad
                ((CalculatorViewModel)DataContext).EnterNumberCommand.Execute(e.Key.ToString().Substring(6));
            }
            else if (e.Key == Key.Add)
            {
                ((CalculatorViewModel)DataContext).EnterBinaryOperatorCommand.Execute("+");
            }
            else if (e.Key == Key.Subtract)
            {
                ((CalculatorViewModel)DataContext).EnterBinaryOperatorCommand.Execute("-");
            }
            else if (e.Key == Key.Multiply)
            {
                ((CalculatorViewModel)DataContext).EnterBinaryOperatorCommand.Execute("*");
            }
            else if (e.Key == Key.Divide)
            {
                ((CalculatorViewModel)DataContext).EnterBinaryOperatorCommand.Execute("/");
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ((CalculatorViewModel)DataContext).EqualsCommand.Execute(null);
            }
            else if (e.Key == Key.OemPeriod)
            {
                ((CalculatorViewModel)DataContext).EnterDotCommand.Execute(null);
            }
            else if (e.Key == Key.Back)
            {
                ((CalculatorViewModel)DataContext).DeleteSymbolCommand.Execute(null);
            }
            else if (e.Key == Key.Escape)
            {
                ((CalculatorViewModel)DataContext).ClearCommand.Execute(null);
            }
            else if (e.Key == Key.Delete)
            {
                ((CalculatorViewModel)DataContext).ClearEntryCommand.Execute(null);
            }
            else if (e.Key == Key.Decimal)
            {
                ((CalculatorViewModel)DataContext).EnterDotCommand.Execute(null);
            }
            else if (e.Key == Key.OemMinus)
            {
                ((CalculatorViewModel)DataContext).InvertCommand.Execute(null);
            }

        }
    }
}