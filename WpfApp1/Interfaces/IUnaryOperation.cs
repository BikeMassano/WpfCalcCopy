namespace WpfApp1.Interfaces
{
    public interface IUnaryOperation
    {
        string GetOperator();
        double Execute(double number);
    }
}
