using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    public class SubtractOperation : IBinaryOperation
    {
        public string Operator => "-";
        public double Execute(double left, double right) => left - right;
    }
}
 