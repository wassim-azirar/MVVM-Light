using System;
using System.Windows.Input;

namespace MvvmLightWP8.Helpers
{
    /// <summary>
    ///     An ICommand whose delegates can be attached for DelegateCommandBase.Execute(System.Object) and DelegateCommandBase.CanExecute(System.Object).
    /// </summary>
    public abstract class DelegateCommandBase : ICommand
    {
        private readonly Func<object, bool> _canExecuteMethod;
        private readonly Action<object> _executeMethod;

        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        /// <summary>
        ///     Determines if the command can execute with the provided parameter by invoing the Func supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>
        ///     Returns true> if the command can execute.  False otherwise.
        /// </returns>
        protected bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        /// <summary>
        ///     Executes the command with the provided parameter by invoking the Action supplied during construction.
        /// </summary>
        /// <param name="parameter" />
        protected void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
