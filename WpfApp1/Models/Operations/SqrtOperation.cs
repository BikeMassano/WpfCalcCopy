using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    class SqrtOperation : IOperation
    {
        public string GetOperator() => "√";
        public int GetPriority() => 3;
        public double Execute(double left, double right) => 1/right;
    }
}
