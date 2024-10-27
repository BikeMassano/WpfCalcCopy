using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class DivideOperation : IOperation
    {
        public string GetOperator() => "/";
        public int GetPriority() => 2;
        public double Execute(double left, double right)
        {
            if (right == 0)
            {
                throw new ArgumentException("Деление на 0 невозможно");
            }

            return left / right;
        }
    }
}
