using WpfApp1.Interfaces;

namespace ConsoleCalculator.Core.Operations
{
    public class PowerOperation : IOperation
    {
        public string GetOperator() => "^";
        public int GetPriority() => 2;
        public double Execute(double left, double right) => Math.Pow(left, right);
    }
}
