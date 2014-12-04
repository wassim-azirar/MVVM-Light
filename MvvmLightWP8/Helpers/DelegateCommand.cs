using System;

namespace MvvmLightWP8.Helpers
{
    public class DelegateCommand : DelegateCommandBase
    {
        /// <summary>
        ///     Creates a new instance of Commands.DelegateCommand with the Action to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The Action to invoke when ICommand.Execute(System.Object) is called.</param>
        public DelegateCommand(Action executeMethod) : this(executeMethod, (() => true))
        {
        }

        /// <summary>
        ///     Creates a new instance of Commands.DelegateCommand with the Action to invoke on execution
        ///     and a Func to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The Action to invoke whenICommand.Execute(System.Object) is called.</param>
        /// <param name="canExecuteMethod">The Func to invoke when ICommand.CanExecute(System.Object) is called</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base((o => executeMethod()), (o => canExecuteMethod()))
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        public void Execute()
        {
            Execute(null);
        }

        
        /// <summary>
        ///     Determines if the command can be executed.
        /// </summary>
        /// <returns>
        ///     Returns true if the command can execute,otherwise returns false.
        /// </returns>
        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }

    /// <summary>
    ///     An ICommand whose delegates can be attached for DelegateCommand.Execute and DelegateCommand.CanExecute.
    /// </summary>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        /// <summary>
        ///     Initializes a new instance of DelegateCommand.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command.
        ///     This can be null to just hook up a CanExecute delegate.
        /// </param>
        /// <remarks>
        ///     CanExecute will always return true.
        /// </remarks>
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, (o => true))
        {
        }

        /// <summary>
        ///     Initializes a new instance of DelegateCommand.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command.  This can be null.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     When both executeMethod and canExecuteMethod are null".
        /// </exception>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base((o => executeMethod((T)o)), (o => canExecuteMethod((T)o)))
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            var type = typeof(T);
            if (type.IsValueType && (!type.IsGenericType || !typeof(Nullable<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
            {
                throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
            }
        }

        /// <summary>
        ///     Determines if the command can execute by invoked the Func provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        ///     true if this command can be executed; otherwise false.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter);
        }

        /// <summary>
        ///     Executes the command and invokes the Action provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(T parameter)
        {
            base.Execute(parameter);
        }
    }
}
