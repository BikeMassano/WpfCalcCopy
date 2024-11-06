using System.Windows.Input;

namespace WpfApp1
{
    internal interface IKeyHandler
    {
        void HandleKeyDown(KeyEventArgs e);
    }
}