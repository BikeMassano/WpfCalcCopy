using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class ModulusOperation : IBinaryOperation
    {
        public string Operator => "%";
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
