using System.Windows.Media.Media3D;
using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    internal class OneDivideXOperation : IUnaryOperation
    {
        /// <summary>
        /// Операция деления числа 1 на число в параметре
        /// </summary>
        public string GetOperator() => "1/x";
        public double Execute(double number)
        {
            if (number == 0)
            {
                throw new ArgumentException("Деление на 0 невозможно.");
            }
            return 1 / number;
        }
    }
}
