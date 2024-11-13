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
        private readonly WindowResizer _windowResizer;
        private readonly IKeyHandler _keyHandler;
        public MainWindow()
        {
            InitializeComponent();

            _windowResizer = new WindowResizer(600);
            _keyHandler = new KeyHandler((CalculatorViewModel)DataContext);

            // Подписка на событие изменения размеров окна
            this.SizeChanged += Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid MyGrid = (Grid)this.FindName("MyGrid");
            _windowResizer.HandleSizeChanged(MyGrid, e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            _keyHandler.HandleKeyDown(e);
        }

    }
}