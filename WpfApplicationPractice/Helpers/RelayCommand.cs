using System;
using System.Windows.Input;

namespace WpfApplicationPractice.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _command;
        private readonly Func<bool> _canExcecute;
        
        // El Uso de delegados permite utilizar metodos directamente desde
        // el viewmodel sin necesidad de crear classes command separadas
        public RelayCommand(Action<object> command, Func<bool> canExcecute)
        {
            _command = command;
            _canExcecute = canExcecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_command == null)
                return false;

            if (_canExcecute != null)
                return _canExcecute.Invoke();

            return true;
        }

        public void Execute(object parameter)
        {
            _command?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}