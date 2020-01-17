using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YourNotes.Models
{
	public class Note
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(40)]
		public string Title { get; set; }

		[Required]
		[MaxLength(200)]
		public string Memo { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		public int UserId { get; set; }
	}
}