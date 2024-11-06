using System.Windows.Controls;
using System.Windows;

namespace WpfApp1.Services
{
    class WindowResizer
    {
        private readonly int thresholdWidth;

        public WindowResizer(int thresholdWidth)
        {
            this.thresholdWidth = thresholdWidth;
        }

        public void HandleSizeChanged(Grid grid, SizeChangedEventArgs e)
        {
            if (grid != null)
            {
                ColumnDefinition secondColumn = (ColumnDefinition)grid.ColumnDefinitions[1];

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
