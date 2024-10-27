using WpfApp1.Models;

namespace WpfApp1.Interfaces
{
    public interface ITokenizer
    {
        // Интерфейс для токенизатора
        IEnumerable<Token> Tokenize(string expression);
    }
}
