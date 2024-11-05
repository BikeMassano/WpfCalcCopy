using WpfApp1.Interfaces;

namespace WpfApp1.Models.Operations
{
    class AddOperation : IBinaryOperation
    {
        public string Operator => "+";

        public double Execute(double left, double right) => left + right;
    }
}
