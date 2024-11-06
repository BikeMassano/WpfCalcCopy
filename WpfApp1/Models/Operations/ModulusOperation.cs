using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    /// <summary>
    /// Операция нахождения остатка от деления двух чисел
    /// </summary>
    class ModulusOperation : IBinaryOperation
    {
        public string GetOperator() => "%";
        public double Execute(double left, double right)
        {
            if (right == 0)
            {
                throw new ArgumentException("Деление на 0 невозможно.");
            }

            return left % right;
        }

        
    }
}
