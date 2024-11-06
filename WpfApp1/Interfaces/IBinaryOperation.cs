namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс для операций
    /// </summary>
    public interface IBinaryOperation
    {
        string GetOperator();
        double Execute(double left, double right);
    }
}
