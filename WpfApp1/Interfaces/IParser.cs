using WpfApp1.Models;

namespace WpfApp1.Interfaces
{
    // Интерфейс для парсера
    public interface IParser
    {
        Node Parse(IEnumerable<Token> tokens);
    }
}
