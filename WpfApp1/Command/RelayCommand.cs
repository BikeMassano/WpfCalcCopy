using System.Windows.Input;

namespace WpfApp1.Utilities
{
    /// <summary>
    /// Здесь появился еще один новый класс: RelayCommand . Это важно для работы MVVM. 
    /// Это команда, которая предназначена для выполнения другими классами для запуска кода в этом классе путем вызова делегатов.
    /// </summary>
    /// <summary>
    /// Команда, единственной целью которой является передача своей функциональности другим объектам 
    /// путем вызова делегатов. Возвращаемое значение по умолчанию для метода CanExecute равно "true".
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion // Fields
        #region Constructors
        /// <summary>
        /// Создает новую команду, которую всегда можно выполнить.
        /// </summary>
        /// <param name="execute">Логика выполнения.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }
        /// <summary>
        /// Создает новую команду.
        /// </summary>
        /// <param name="execute">Логика выполнения.</param>
        /// <param name="canExecute">Логика состояния выполнения</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors
        #region ICommand Members
        /// <summary>
        /// Определяет, может ли команда выполняться
        /// При использовании RelayCommand вы можете указать два метода. 
        /// Первый метод является основным методом, который вы хотите запускать при вызове команды. 
        /// Второй метод, который вы используете для добавления проверок, таких как validation, 
        /// и который должен возвращать bool . если он возвращает false , то основной метод не будет запущен.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool CanExecute(object parameters)
        {
            return _canExecute == null ? true : _canExecute(parameters);
        }
        /// <summary>
        /// Вызывается при изменении условий, указывающий, может ли команда выполняться.
        /// Для этого используется событие CommandManager.RequerySuggested.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// Выполняет логику команды
        /// </summary>
        /// <param name="parameters"></param>
        public void Execute(object parameters)
        {
            _execute(parameters);
        }
        #endregion // ICommand Members
        #region Комментарии
        // Для взаимодействия пользователя и приложения в MVVM используются команды.
        // Это не значит, что вовсе не можем использовать события и событийную модель,
        // однако везде, где возможно, вместо событий следует использовать команды.

        //В WPF команды представлены интерфейсом ICommand:


        // Класс реализует два метода:

        // CanExecute: определяет, может ли команда выполняться

        // Execute: собственно выполняет логику команды

        // Событие CanExecuteChanged вызывается при изменении условий, указывающий, может ли команда выполняться.
        // Для этого используется событие CommandManager.RequerySuggested.

        // Ключевым является метод Execute. Для его выполнения в конструкторе команды передается делегат типа Action<object>.
        // При этом класс команды не знает какое именно действие будет выполняться.
        #endregion
    }
}

