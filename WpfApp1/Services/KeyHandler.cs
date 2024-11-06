using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    class KeyHandler : IKeyHandler
    {
        private readonly CalculatorViewModel _viewModel;

        public KeyHandler(CalculatorViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            if (!_viewModel.IsOperationEnabled) return;

            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                    _viewModel.EnterNumberCommand.Execute(e.Key.ToString().Substring(1));
                    break;

                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    _viewModel.EnterNumberCommand.Execute(e.Key.ToString().Substring(6));
                    break;

                case Key.Add:
                    _viewModel.EnterBinaryOperatorCommand.Execute("+");
                    break;

                case Key.Subtract:
                    _viewModel.EnterBinaryOperatorCommand.Execute("-");
                    break;

                case Key.Multiply:
                    _viewModel.EnterBinaryOperatorCommand.Execute("*");
                    break;

                case Key.Divide:
                    _viewModel.EnterBinaryOperatorCommand.Execute("/");
                    break;

                case Key.Enter:
                    _viewModel.EqualsCommand.Execute(null);
                    break;

                case Key.OemPeriod:
                    _viewModel.EnterDotCommand.Execute(null);
                    break;

                case Key.Back:
                    _viewModel.DeleteSymbolCommand.Execute(null);
                    break;

                case Key.Escape:
                    _viewModel.ClearCommand.Execute(null);
                    break;

                case Key.Delete:
                    _viewModel.ClearEntryCommand.Execute(null);
                    break;

                case Key.Decimal:
                    _viewModel.EnterDotCommand.Execute(null);
                    break;

                case Key.OemMinus:
                    _viewModel.InvertCommand.Execute(null);
                    break;
            }
        }
    }
}
