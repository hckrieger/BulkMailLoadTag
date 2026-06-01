using BulkMailLoadTagApp.Model;
using BulkMailLoadTagApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Printing;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace BulkMailLoadTagApp.ViewModel
{
	public class BulkMailTagVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public DataToPdfCommand DataToPdfCommand { get; set; }
		private BulkMailTagData? data;
		public BulkMailTagData? Data => data;

		private readonly string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BulkMailTagData.json");


		public string DispatchNumber
		{
			get => data?.DispatchNumber ?? string.Empty;
			set
			{
				data?.DispatchNumber = value;
				OnPropertyChanged(nameof(DispatchNumber));
			}
		}

		public string JobNumber
		{
			get  => data?.JobNumber ?? string.Empty;
			set 
			{
				data?.JobNumber = value;
				OnPropertyChanged(nameof(JobNumber));
			}
		}

		public string CustomerNameTitle
		{
			get => data?.CustomerNameTitle ?? string.Empty;
			set
			{
				data?.CustomerNameTitle = value;
				OnPropertyChanged(nameof(CustomerNameTitle));
			}
		}

		public string Version
		{
			get => data?.Version ?? string.Empty;
			set
			{
				data?.Version = value;
				OnPropertyChanged(nameof(Version));
			}
		}
	

		public string SkidQuantity
		{
			get => data?.SkidQuantity ?? string.Empty;
			set
			{
				data?.SkidQuantity = value;
				OnPropertyChanged(nameof(SkidQuantity));
			}
		}

		public string Classification
		{
			get => data?.Classification ?? string.Empty;
			set
			{
				data?.Classification = value;
				OnPropertyChanged(nameof(Classification));
			}
		}

		public DateTime Date
		{
			get => data?.Date ?? DateTime.Now;
			set
			{
				
					data?.Date = value;
					OnPropertyChanged(nameof(Date));
				
			}
		}

		public string ShiftInitials
		{
			get => data?.ShiftInitials ?? string.Empty;
			set
			{
				data?.ShiftInitials = value;
				OnPropertyChanged(nameof(ShiftInitials));
			}
		}

		public string Comments
		{
			get => data?.Comments ?? string.Empty;
			set
			{
				data?.Comments = value;
				OnPropertyChanged(nameof(Comments));
			}
		}


		public string[] ClassificationType { get; set; }

		public BulkMailTagVM()
		{
			ClassificationType = ["Standard", "Periodical", "Bounded Printed Matter", "1st Class", "Periodical Letter", "Standard Letter", "1st Class Letter"];
			PropertyChanged += (sender, e) =>
			{
				

			    SaveData();
				
			};

			ReadData();

			DataToPdfCommand = new DataToPdfCommand(this);
		}

		private void ReadData()
		{
			if (File.Exists(jsonPath))
			{
				string jsonString = File.ReadAllText(jsonPath);
				try
				{
					data = JsonSerializer.Deserialize<BulkMailTagData>(jsonString);

				}
				catch (JsonException ex)
				{
					Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
					data = new BulkMailTagData();
					SaveData();
					return;
				}
			}
			else
			{
				data = new BulkMailTagData();
				SaveData();
			}
		}



		private void SaveData()
		{
			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(data, options);
			File.WriteAllText(jsonPath, jsonString);


		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		
		}

		internal void DataToPdf()
		{
			MessageBox.Show("Weeeeee!");
		}
	}

	
}
