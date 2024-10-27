using WpfApp1.Enums;

namespace WpfApp1.Models
{
    /// <summary>
    /// Класс токена
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public class Token(TokenType type, string value)
    {
        public TokenType Type { get; } = type;
        public string Value { get; } = value;
        public override bool Equals(object obj)
        {
            if (obj is Token other)
            {
                return Type == other.Type && Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }
    }
}
