using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    class AddOperation : IOperation
    {
        public string GetOperator() => "+";
        public int GetPriority() => 1;
        public double Execute(double left, double right) => left + right;
    }
}
