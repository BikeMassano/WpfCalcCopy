using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class MultiplyOperation : IBinaryOperation
    {
        public string Operator => "*";
        public double Execute(double left, double right) => left * right;
    }
}
