namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс для операций
    /// </summary>
    interface IBinaryOperation : IOperation
    {
        double Execute(double left, double right);
    }
}
