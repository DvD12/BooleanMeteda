using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlogWPF.ViewModels
{
	public class MyCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public MyCommand(Action execute)
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
			_execute();
		}
	}
}
