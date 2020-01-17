using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourNotes
{
	public class UserInfo
	{
		public int UserId { get; set; }

		public string Email { get; set; }

		public UserInfo(int id, string email)
		{
			UserId = id;
			Email = email;
		}
	}
}