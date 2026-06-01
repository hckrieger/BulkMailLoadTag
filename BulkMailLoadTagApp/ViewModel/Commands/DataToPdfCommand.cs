using BulkMailLoadTagApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BulkMailLoadTagApp.ViewModel.Commands
{
	public class DataToPdfCommand : ICommand	
	{
		public BulkMailTagVM? VM { get; set; }

		public event EventHandler? CanExecuteChanged;

		private string[] messages;
		public DataToPdfCommand(BulkMailTagVM vm)
		{
			VM = vm;

			messages =
			[
				"Skid Quantity must be a number greater than 0.",
				"Dispatch Number cannot be empty.",
				"Job Number cannot be empty.",
				"Customer Name/Job Name cannot be empty.",
				"Shift/Initials cannot be empty."
			];
		}
		public bool CanExecute(object? parameter)
		{

			// error catch the string to int parsing later




			
			return true;
		}

		public void Execute(object? parameter)
		{
			BulkMailTagData? data = parameter as BulkMailTagData;
			var errors = new List<string>();
			if (data != null)
			{

				if (!int.TryParse(data.SkidQuantity, out int skidQty) || skidQty <= 0)
					errors.Add(messages[0]);
				if (string.IsNullOrWhiteSpace(data.DispatchNumber))
					errors.Add(messages[1]);
				if (string.IsNullOrWhiteSpace(data.JobNumber))
					errors.Add(messages[2]);
				if (string.IsNullOrWhiteSpace(data.CustomerNameTitle))
					errors.Add(messages[3]);
				if (string.IsNullOrWhiteSpace(data.ShiftInitials))
					errors.Add(messages[4]);

				if (errors.Count > 0)
				{
					string messageStrring = "Errors: \n";
				
					foreach (var error in errors)
					{
						messageStrring += $"-{error}\n";
					}

					MessageBox.Show(messageStrring, "Input Validation Errors", MessageBoxButton.OK, MessageBoxImage.Error);
				} else
				{
					VM?.DataToPdf();

				}




			}
		}
	}
}
