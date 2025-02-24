using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlogWPF.ViewModels
{
	public class GenericCommand<T> : ICommand
	{
		private readonly Action<T> _execute; // Action<T> rappresenta una funzione void che prende un parametro di tipo T in input
		private readonly Func<bool> _canExecute;

		public GenericCommand(Action<T> execute)
		{
			_execute = execute;
			_canExecute = () => true;
		}

		public event EventHandler CanExecuteChanged;
		public bool CanExecute(object parameter)
		{
			return _canExecute();
		}

		public void Execute(object parameter)
		{
			// Convert(...) serve solo per convertire l'object generico nel tipo T
			_execute((T)Convert.ChangeType(parameter, typeof(T)));
		}
	}
}
