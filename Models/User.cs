using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YourNotes.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password")]
		[Required]
		[NotMapped]
		public string ConfirmPassword { get; set; }

		public virtual List<Note> Notes { get; set; }
	}
}