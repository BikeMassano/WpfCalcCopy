using WpfApp1.Models;

namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс для вычислителя
    /// </summary>
    public interface IEvaluator
    {
        double Evaluate(Node expression);
    }
}
