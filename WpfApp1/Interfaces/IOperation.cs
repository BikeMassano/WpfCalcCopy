namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс для операций
    /// </summary>
    public interface IOperation
    {
        double Execute(double left, double right);
        string GetOperator();
        int GetPriority();
    }
}
