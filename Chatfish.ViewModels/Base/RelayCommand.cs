using System;
using System.Windows.Input;

namespace Chatfish.ViewModels
{
    /// <summary>A basic command that runs an action</summary>
    public class RelayCommand : ICommand
    {
        /// <summary>The action to run
        private Action mAction;

        /// <summary>The event thats fired when the CanExecute(object) value has changed</summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        
        /// <summary>Default constructor</summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>A relay command can always execute</summary>
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}