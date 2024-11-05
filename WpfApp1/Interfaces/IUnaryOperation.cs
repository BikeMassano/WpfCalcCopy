namespace WpfApp1.Interfaces
{
    public interface IUnaryOperation
    {
        string Operator { get; }
        double Execute(double number);
    }
}
