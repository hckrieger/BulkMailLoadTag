using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BulkMailLoadTagApp.Model
{
	public class BulkMailTagData
	{
		[JsonPropertyName("dispatch number")]
		public string DispatchNumber { get; set; } = string.Empty;
		[JsonPropertyName("job number")]
		public string JobNumber { get; set; } = string.Empty;
		[JsonPropertyName("customer name/job name")]
		public string CustomerNameTitle { get; set; } = string.Empty;
		[JsonPropertyName("version")]
		public string Version { get; set; } = string.Empty;
		[JsonPropertyName("skid quantity")]
		public string SkidQuantity { get; set; } = string.Empty;
		[JsonPropertyName("classification")]
		public string Classification { get; set; } = string.Empty;
		[JsonPropertyName("date")]
		public DateTime Date { get; set; } = DateTime.Now;
		[JsonPropertyName("shift/initials")]
		public string ShiftInitials { get; set; } = string.Empty;
		[JsonPropertyName("comments")]
		public string Comments { get; set; } = string.Empty;

	}
}
