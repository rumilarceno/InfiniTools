using System;
using System.Windows.Input;

namespace InfinitTools.Commands
{
    class CommandHandler : ICommand
    {
        private Action _action = null;
        private bool _canExecute = false;
        public event EventHandler CanExecuteChanged;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }
}
