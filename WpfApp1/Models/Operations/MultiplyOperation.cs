using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class MultiplyOperation : IOperation
    {
        public string GetOperator() => "*"; 
        public int GetPriority() => 2;
        public double Execute(double left, double right) => left * right;
    }
}
