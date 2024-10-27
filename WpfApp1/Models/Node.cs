namespace WpfApp1.Models
{
    /// <summary>
    /// Класс узла дерева
    /// </summary>
    /// <param name="token"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public class Node(Token token, Node left = null, Node right = null)
    {
        public Token Token { get; } = token;
        public Node Left { get; } = left;
        public Node Right { get; } = right;

        public override bool Equals(object obj)
        {
            if (obj is Node other)
            {
                return Token.Equals(other.Token) &&
                       Equals(Left, other.Left) &&
                       Equals(Right, other.Right);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Token, Left, Right);
        }

    }
}
