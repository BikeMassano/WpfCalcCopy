using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class PowerOperation : IOperation
    {
        public string GetOperator() => "^";
        public int GetPriority() => 3;
        public double Execute(double left, double right) => Math.Pow(left, right);
    }
}
