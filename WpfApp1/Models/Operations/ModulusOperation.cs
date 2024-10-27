using WpfApp1.Interfaces;

namespace ConsoleCalculator.Core.Operations
{
    public class ModulusOperation : IOperation
    {
        public string GetOperator()
        {
            return "%";
        }

        public int GetPriority() => 2;

        public double Execute(double left, double right)
        {
            if (right == 0)
            {
                throw new ArgumentException("Division by zero");
            }

            return left % right;
        }

        
    }
}
