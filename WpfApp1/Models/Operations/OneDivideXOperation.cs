using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    internal class OneDivideXOperation : IUnaryOperation
    {
        public string Operator => "1/x";
        public double Execute(double number) => 1/number;
    }
}
